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
            services.AddScoped<IChildRepository, ChildRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            //services.AddScoped<IBookingRepository, BookingRepository>();
            //services.AddScoped<IDonationRepository, DonationRepository>();
            //services.AddScoped<IDonationDetailRepository, DonationDetailRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVillageService, VillageService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            //services.AddScoped<IBookingService, BookingService>();
            //services.AddScoped<IDonationService, DonationService>();
            //services.AddScoped<IDonationDetailService, DonationDetailService>();
            return services;
        }
    }
}
