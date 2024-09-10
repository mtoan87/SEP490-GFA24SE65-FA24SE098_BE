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
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            
            return services;
        }
    }
}
