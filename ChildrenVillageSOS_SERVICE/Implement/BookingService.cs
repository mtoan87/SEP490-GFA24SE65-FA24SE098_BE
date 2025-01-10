using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.SendEmail;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ISendService _sendService;
        public BookingService(IBookingRepository bookingRepository, ISendService sendService)
        {
            _bookingRepository = bookingRepository;
            _sendService = sendService;
        }
        public async Task<BookingResponse[]> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }
        public async Task<BookingResponse[]> GetBookingsWithSlotsByUserAsync(string userAccountId)
        {
            return await _bookingRepository.GetBookingsWithSlotsByUserAsync(userAccountId);
        }
        public async Task<bool> CreateBookingAsync(BookingRequest request)
        {
            
            var existingBooking = await _bookingRepository.GetBookingBySlotAsync(request.HouseId, request.Visitday, request.BookingSlotId);
            if (existingBooking != null)
            {
                throw new InvalidOperationException($"Slot {request.BookingSlotId} for {request.Visitday:yyyy-MM-dd} in house {request.HouseId} is already booked.");
            }

            var newBooking = new Booking
            {
                HouseId = request.HouseId,
                Visitday = request.Visitday,
                BookingSlotId = request.BookingSlotId,
                UserAccountId = request.UserAccountId,
                Status = "Pending",
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await _bookingRepository.AddAsync(newBooking);
            return true;
        }

        public async Task<Booking> CreateBooking(CreateBookingDTO createBooking)
        {
            var newBooking = new Booking
            {
                HouseId = createBooking.HouseId,
                UserAccountId = createBooking.UserAccountId,
                BookingSlotId = createBooking.BookingSlotId,
                Visitday = createBooking.Visitday,
                Status = "Pending",
                CreatedDate = DateTime.Now
            };
            await _bookingRepository.AddAsync(newBooking);
            return newBooking;
        }

        public async Task<Booking> DeleteBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }
            booking.IsDeleted = true;
            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<Booking> RestoreBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }
            if (booking.IsDeleted == true) 
            {
                booking.IsDeleted = false;
                await _bookingRepository.UpdateAsync(booking);
            }
            return booking;
        }

        public async Task<Booking> UpdateBooking(int id, UpdateBookingDTO updateBooking)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }

            booking.HouseId = updateBooking.HouseId;
            booking.UserAccountId = updateBooking.UserAccountId;
            booking.BookingSlotId = updateBooking.BookingSlotId;
            booking.Visitday = updateBooking.Visitday;
            booking.Status = updateBooking.Status;
            booking.ModifiedDate = DateTime.Now;
            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }

        public async Task<bool> NotifySponsorForVisit(int bookingId)
        {
            // Lấy thông tin chi tiết chuyến thăm từ Repository
            var bookingDetails = await _bookingRepository.GetBookingDetailsAsync(bookingId);

            if (bookingDetails == null)
            {
                Console.WriteLine("Booking details not found.");
                return false;
            }

            var userName = "ChildrenVillageSOS";
            var emailFrom = "toancx202@gmail.com";
            var password = "taio jkdc phja ptkq";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", bookingDetails.UserEmail));
            message.Subject = "Your Visit to ChildrenVillageSOS";

            message.Body = new TextPart("html")
            {
                Text =
                $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f8f9fa;
                            padding: 20px;
                        }}
                        .container {{
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            font-size: 24px;
                            color: #007bff;
                            margin-bottom: 20px;
                        }}
                        .content {{
                            font-size: 16px;
                            color: #333;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 14px;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>Dear {bookingDetails.UserEmail},</div>
                        <div class='content'>
                            <p>We are excited to confirm your visit to our ChildrenVillageSOS facility. Below are the details of your visit:</p>
                            <ul>
                                <li><b>Village Name:</b> {bookingDetails.VillageName}</li>
                                <li><b>Village Address:</b> {bookingDetails.VillageLocation}</li>
                                <li><b>House Name:</b> {bookingDetails.HouseName}</li>
                                <li><b>House Address:</b> {bookingDetails.HouseLocation}</li>
                                <li><b>Visit Date:</b> {bookingDetails.Visitday?.ToString("yyyy-MM-dd")}</li>
                                <li><b>Visit Time:</b> {bookingDetails.StartTime?.ToString("HH:mm")} - {bookingDetails.EndTime?.ToString("HH:mm")}</li>
                            </ul>
                            <p>We look forward to your visit and thank you for your continued support of our mission.</p>
                        </div>
                        <div class='footer'>
                            <p>Warm regards,</p>
                            <p>The ChildrenVillageSOS Team</p>
                        </div>
                    </div>
                </body>
            </html>
            "
            };

            using var client = new SmtpClient();
            try
            {
                // Kết nối tới máy chủ SMTP của Gmail
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailFrom, password);

                // Gửi email
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }            
        }
        public async Task<Booking> ConfirmBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }

            
            booking.Status = "Confirmed";
            booking.ModifiedDate = DateTime.Now;
            await _bookingRepository.UpdateAsync(booking);

            await NotifySponsorForVisit(id);
            return booking;
        }

        public async Task<List<Booking>> SearchBookings(SearchBookingDTO searchBookingDTO)
        {
            return await _bookingRepository.SearchBookings(searchBookingDTO);
        }
    }
}
