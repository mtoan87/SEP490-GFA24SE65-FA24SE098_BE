using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;

namespace ChildrenVillageSOS_API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IBookingSlotRepository, BookingSlotRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IChildRepository, ChildRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ISystemWalletRepository, SystemWalletRepository>();
            services.AddScoped<IHealthWalletRepository, HealthWalletRepository>();
            services.AddScoped<IFoodStuffWalletRepository, FoodStuffWalletRepository>();
            services.AddScoped<IFacilitiesWalletRepository, FacilitiesWalletRepository>();
            services.AddScoped<INecessitiesWalletRepository, NecessitiesWalletRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ISystemWalletService, SystemWalletService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingSlotService, BookingSlotService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IHealthWalletService, HealthWalletService>();
            services.AddScoped<IFoodStuffWalletService, FoodStuffWalletService>();
            services.AddScoped<IFacilitiesWalletService, FacilitiesWalletService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<INecessitiesWalletService, NecessitiesWalletService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IImageService, ImageService>();
            return services;
        }
    }
}
