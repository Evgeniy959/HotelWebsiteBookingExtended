using HotelWebsiteBooking.Data;
using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Service.CommentService;
using HotelWebsiteBooking.Service.DateService;
using HotelWebsiteBooking.Service.EmailService;
using HotelWebsiteBooking.Service.RoomService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IDaoRoom, DbDaoRoom>();
builder.Services.AddTransient<IDaoComment, DbDaoComment>();
builder.Services.AddSingleton<DaoDate>();
builder.Services.AddSingleton<DaoGuest>();

builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.SetApiKey(builder.Configuration.GetSection("Stripe")["SecretKey"]);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
