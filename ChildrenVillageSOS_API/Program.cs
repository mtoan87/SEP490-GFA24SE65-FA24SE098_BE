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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy
                .AllowAnyOrigin()    // Cho phép m?i ngu?n g?c
                .AllowAnyHeader()    // Cho phép m?i header
                .AllowAnyMethod();   // Cho phép m?i ph??ng th?c (GET, POST, PUT, DELETE, etc.)
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
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
