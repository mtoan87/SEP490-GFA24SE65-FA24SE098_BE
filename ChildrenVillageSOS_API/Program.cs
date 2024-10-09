using ChildrenVillageSOS_API.Configuration;
using ChildrenVillageSOS_DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SoschildrenVillageDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionStringDB");
    options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
});

builder.Services.AddRepository();
builder.Services.AddService();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:7073", "http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

// Ensure CORS is configured before authentication and authorization
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
