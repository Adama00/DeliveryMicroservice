using BackEnd.Data;
using BackEnd.Service.Interface;
using BackEnd.Service.Provider;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDeliveryMicroservice, DeliveryPgService>();
builder.Services.AddControllers();

builder.Services.AddDbContext<DeliveryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                      o => o.UseNetTopologySuite()));

builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options=>
{
    options.AddPolicy("CorsPolicy", policyBuilder =>
{
    policyBuilder.WithOrigins(new[] { "http://localhost:3000" });
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
});
    options.AddPolicy("All", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();

app.UseHttpsRedirection();
app.UseCors("All");

app.UseAuthorization();

app.MapControllers();

app.Run();
