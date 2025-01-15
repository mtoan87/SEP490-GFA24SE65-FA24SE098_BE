using ChildrenVillageSOS_DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class EventStatusUpdaterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventStatusUpdaterService> _logger;

        public EventStatusUpdaterService(IServiceProvider serviceProvider, ILogger<EventStatusUpdaterService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EventStatusUpdaterService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Kiểm tra và cập nhật trạng thái sự kiện
                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<SoschildrenVillageDbContext>();

                    var now = DateTime.UtcNow;
                    var expiredEvents = dbContext.Events
                        .Where(e => e.EndTime < now && e.Status != "Close" && !e.IsDeleted)
                        .ToList();

                    foreach (var eventItem in expiredEvents)
                    {
                        // Cập nhật trạng thái của Event thành "Close"
                        eventItem.Status = "Close";
                        eventItem.ModifiedDate = now; // Cập nhật thời gian chỉnh sửa

                        // Lấy Expense liên quan đến Event có Status = "OnEvent" hoặc "Approved"
                        var relatedExpense = dbContext.Expenses
                            .Where(e => e.EventId == eventItem.Id && (e.Status == "OnEvent" || e.Status == "Approved"))
                            .FirstOrDefault();

                        if (relatedExpense != null)
                        {
                            // Gán CurrentAmount từ Event vào AmountReceive của Expense
                            relatedExpense.AmountReceive = eventItem.CurrentAmount ?? 0;

                            // Nếu trạng thái của Expense là "OnEvent", thực hiện trừ tiền từ các ví
                            if (relatedExpense.Status == "OnEvent")
                            {
                                // Trừ tiền từ các ví của sự kiện
                                if (eventItem.FacilitiesWalletId.HasValue)
                                {
                                    var facilitiesWallet = await dbContext.FacilitiesWallets.FindAsync(eventItem.FacilitiesWalletId.Value);
                                    if (facilitiesWallet != null)
                                    {
                                        facilitiesWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                                        dbContext.FacilitiesWallets.Update(facilitiesWallet);
                                    }
                                }

                                if (eventItem.FoodStuffWalletId.HasValue)
                                {
                                    var foodStuffWallet = await dbContext.FoodStuffWallets.FindAsync(eventItem.FoodStuffWalletId.Value);
                                    if (foodStuffWallet != null)
                                    {
                                        foodStuffWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                                        dbContext.FoodStuffWallets.Update(foodStuffWallet);
                                    }
                                }

                                if (eventItem.HealthWalletId.HasValue)
                                {
                                    var healthWallet = await dbContext.HealthWallets.FindAsync(eventItem.HealthWalletId.Value);
                                    if (healthWallet != null)
                                    {
                                        healthWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                                        dbContext.HealthWallets.Update(healthWallet);
                                    }
                                }

                                if (eventItem.NecessitiesWalletId.HasValue)
                                {
                                    var necessitiesWallet = await dbContext.NecessitiesWallets.FindAsync(eventItem.NecessitiesWalletId.Value);
                                    if (necessitiesWallet != null)
                                    {
                                        necessitiesWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                                        dbContext.NecessitiesWallets.Update(necessitiesWallet);
                                    }
                                }
                            }

                            // Cập nhật trạng thái Expense thành "Approved"
                            relatedExpense.Status = "Approved";
                            relatedExpense.ModifiedDate = DateTime.Now;

                            dbContext.Expenses.Update(relatedExpense);
                        }
                    }

                    if (expiredEvents.Any())
                    {
                        // Lưu thay đổi vào cơ sở dữ liệu
                        await dbContext.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation($"Updated {expiredEvents.Count} events to 'Close' and expenses to 'Approved'.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating event statuses.");
                }

                // Chờ một khoảng thời gian trước khi lặp lại (Ví dụ: 5 phút)
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }

            _logger.LogInformation("EventStatusUpdaterService is stopping.");
        }
    }
}
