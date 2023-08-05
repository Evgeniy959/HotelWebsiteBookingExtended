using HotelAdmin.Models;
using HotelAdmin.Service.CategoryService;
using HotelAdmin.Service.CategoryTariffService;
using HotelAdmin.Service.ClientService;
using HotelAdmin.Service.CommentService;
using HotelAdmin.Service.EmailService;
using HotelAdmin.Service.OrderService;
using HotelAdmin.Service.RoomDateService;
using HotelAdmin.Service.RoomService;
//using HotelAdmin.Service.RoomTariffService;
using HotelAdmin.Service.SubscriberService;
using HotelAdmin.Service.TariffPlanService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDaoRoom, DbDaoRoom>();
builder.Services.AddTransient<IDaoCategory, DbDaoCategory>();
builder.Services.AddTransient<IDaoCategoryTariff, DbDaoCategoryTariff>();
builder.Services.AddTransient<IDaoTariffPlan, DbDaoTariffPlan>();
builder.Services.AddTransient<IDaoClient, DbDaoClient>();
builder.Services.AddTransient<IDaoOrder, DbDaoOrder>();
builder.Services.AddTransient<IDaoOrderPay, DbDaoOrderPay>();
builder.Services.AddTransient<IDaoRoomDate, DbDaoRoomDate>();
builder.Services.AddTransient<IDaoComment, DbDaoComment>();
builder.Services.AddTransient<IDaoSubscriber, DbDaoSubscriber>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { 
        options.LoginPath = "/Access/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
