using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChildrenVillageSOS_API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IAcademicReportRepository, AcademicReportRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IBookingSlotRepository, BookingSlotRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IChildRepository, ChildRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();           
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<ISystemWalletRepository, SystemWalletRepository>();
            services.AddScoped<IHealthWalletRepository, HealthWalletRepository>();
            services.AddScoped<IFoodStuffWalletRepository, FoodStuffWalletRepository>();
            services.AddScoped<IFacilitiesWalletRepository, FacilitiesWalletRepository>();
            services.AddScoped<INecessitiesWalletRepository, NecessitiesWalletRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IHealthReportRepository, HealthReportRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISubjectDetailRepository, SubjectDetailRepository>();
            services.AddScoped<IChildNeedRepository, ChildNeedRepository>();
            services.AddScoped<IChildProgressRepository, ChildProgressRepository>();
            services.AddScoped<ITransferRequestRepository, TransferRequestRepository>();
            services.AddScoped<ITransferHistoryRepository, TransferHistoryRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAcademicReportService, AcademicReportService>();
            services.AddScoped<ISystemWalletService, SystemWalletService>();          
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingSlotService, BookingSlotService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IDonationService, DonationService>();         
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IHealthWalletService, HealthWalletService>();
            services.AddScoped<IFoodStuffWalletService, FoodStuffWalletService>();
            services.AddScoped<IFacilitiesWalletService, FacilitiesWalletService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<INecessitiesWalletService, NecessitiesWalletService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IHealthReportService, HealthReportService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISubjectDetailService, SubjectDetailService>();
            services.AddScoped<IChildNeedService, ChildNeedService>();
            services.AddScoped<IChildProgressService, ChildProgressService>();
            services.AddScoped<ITransferRequestService, TransferRequestService>();
            services.AddScoped<ITransferHistoryService, TransferHistoryService>();
            return services;
        }

        public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SoschildrenVillageDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStringDB")));
        }
    }
}
