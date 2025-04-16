using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Data.Repositories;
using server.Data.Repositories.Interfaces;
using server.Services;
using server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketWizard.Server v1");
    c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();