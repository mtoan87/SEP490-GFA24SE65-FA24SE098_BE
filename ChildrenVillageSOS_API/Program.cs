using ChildrenVillageSOS_API.Configuration;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SoschildrenVillageDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionStringDB");
    options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
});
builder.Services.AddRepository();
builder.Services.AddService();
// Add repository 
//builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
//builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
//builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
//// Add services to the container.
//builder.Services.AddScoped<IPaymentService, PaymentService>();
//builder.Services.AddScoped<IExpenseService, ExpenseService>();
//builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
