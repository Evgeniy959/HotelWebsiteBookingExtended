using HotelAdmin.Models.Entity;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<RoomDate> Dates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPay> OrderPays { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CategoryTariff> TariffAdmins { get; set; }
        public DbSet<TariffPlan> TariffPlans { get; set; }
        /*public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPay> BookingPays { get; set; }*/
        public DbSet<LoginModel> LoginModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginModel>().HasData(
                new LoginModel
                {
                    Email = "admin@mail.ru",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = Role.Administrator
                },
                new LoginModel
                {
                    Email = "manager@mail.ru",
                    Password = BCrypt.Net.BCrypt.HashPassword("manager"),
                    Role = Role.BookingManager
                });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Photo = "img/room/room-s.jpeg",
                    Name = "Стандарт",
                    Square = "20 кв.м",
                    PersonsCount = 2,
                    Path = "Standard"
                },
                new Category
                {
                    Id = 2,
                    Photo = "img/room/room-sb.jpg",
                    Name = "Стандарт \"queen-size\"",
                    Square = "20 кв.м",
                    PersonsCount = 2,
                    Path = "StandardBig"
                },
                new Category
                {
                    Id = 3,
                    Photo = "img/room/room-si.jpeg",
                    Name = "Комфорт",
                    Square = "25 кв.м",
                    PersonsCount = 2,
                    Path = "StandardGood"
                },
                new Category
                {
                    Id = 4,
                    Photo = "img/room/room-sbi.jpg",
                    Name = "Комфорт \"queen-size\"",
                    Square = "25 кв.м",
                    PersonsCount = 2,
                    Path = "StandardGoodBig"
                },
                new Category
                {
                    Id = 5,
                    Photo = "img/room/room-pl.jpg",
                    Name = "Полулюкс",
                    Square = "32 кв.м",
                    PersonsCount = 4,
                    Path = "SemiLuxury"
                },
                new Category
                {
                    Id = 6,
                    Photo = "img/room/room-l.jpg",
                    Name = "Люкс",
                    Square = "46 кв.м",
                    PersonsCount = 4,
                    Path = "Luxury"
                });

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Number = 204,
                    CategoryId = 1
                },
                new Room
                {
                    Id = 2,
                    Number = 307,
                    CategoryId = 2
                },
                new Room
                {
                    Id = 3,
                    Number = 405,
                    CategoryId = 3
                },
                new Room
                {
                    Id = 4,
                    Number = 412,
                    CategoryId = 4
                },
                new Room
                {
                    Id = 5,
                    Number = 514,
                    CategoryId = 5
                },
                new Room
                {
                    Id = 6,
                    Number = 618,
                    CategoryId = 6
                });

            modelBuilder.Entity<RoomDate>().HasData(
                new RoomDate { Id = 1, RoomId = 1 },
                new RoomDate { Id = 2, RoomId = 2 },
                new RoomDate { Id = 3, RoomId = 3 },
                new RoomDate { Id = 4, RoomId = 4 },
                new RoomDate { Id = 5, RoomId = 5 },
                new RoomDate { Id = 6, RoomId = 6 }
                );
            
            modelBuilder.Entity<TariffPlan>().HasData(
                new TariffPlan { Id = 1, Description = "Без питания" },
                new TariffPlan { Id = 2, Description = "Завтрак включён" },
                new TariffPlan { Id = 3, Description = "Полупансион" },
                new TariffPlan { Id = 4, Description = "Полный пансион" }
                );

            modelBuilder.Entity<CategoryTariff>().HasData(
                new CategoryTariff
                {
                    Id = 1,
                    CategoryId = 1,
                    TariffPlanId = 1,
                    Price = 3000
                },
                new CategoryTariff
                {
                    Id = 2,
                    CategoryId = 1,
                    TariffPlanId = 2,
                    Price = 3800
                },
                new CategoryTariff
                {
                    Id = 3,
                    CategoryId = 1,
                    TariffPlanId = 3,
                    Price = 4600
                },
                new CategoryTariff
                {
                    Id = 4,
                    CategoryId = 1,
                    TariffPlanId = 4,
                    Price = 5400
                },
                new CategoryTariff
                {
                    Id = 5,
                    CategoryId = 2,
                    TariffPlanId = 1,
                    Price = 3400
                },
                new CategoryTariff
                {
                    Id = 6,
                    CategoryId = 2,
                    TariffPlanId = 2,
                    Price = 4300
                },
                new CategoryTariff
                {
                    Id = 7,
                    CategoryId = 2,
                    TariffPlanId = 3,
                    Price = 5200
                },
                new CategoryTariff
                {
                    Id = 8,
                    CategoryId = 2,
                    TariffPlanId = 4,
                    Price = 6200
                },
                new CategoryTariff
                {
                    Id = 9,
                    CategoryId = 3,
                    TariffPlanId = 1,
                    Price = 4100
                },
                new CategoryTariff
                {
                    Id = 10,
                    CategoryId = 3,
                    TariffPlanId = 2,
                    Price = 5300
                },
                new CategoryTariff
                {
                    Id = 11,
                    CategoryId = 3,
                    TariffPlanId = 3,
                    Price = 6300
                },
                new CategoryTariff
                {
                    Id = 12,
                    CategoryId = 3,
                    TariffPlanId = 4,
                    Price = 7400
                },
                new CategoryTariff
                {
                    Id = 13,
                    CategoryId = 4,
                    TariffPlanId = 1,
                    Price = 4500
                },
                new CategoryTariff
                {
                    Id = 14,
                    CategoryId = 4,
                    TariffPlanId = 2,
                    Price = 5400
                },
                new CategoryTariff
                {
                    Id = 15,
                    CategoryId = 4,
                    TariffPlanId = 3,
                    Price = 6200
                },
                new CategoryTariff
                {
                    Id = 16,
                    CategoryId = 4,
                    TariffPlanId = 4,
                    Price = 7200
                },
                new CategoryTariff
                {
                    Id = 17,
                    CategoryId = 5,
                    TariffPlanId = 1,
                    Price = 5300
                },
                new CategoryTariff
                {
                    Id = 18,
                    CategoryId = 5,
                    TariffPlanId = 2,
                    Price = 6400
                },
                new CategoryTariff
                {
                    Id = 19,
                    CategoryId = 5,
                    TariffPlanId = 3,
                    Price = 7500
                },
                new CategoryTariff
                {
                    Id = 20,
                    CategoryId = 5,
                    TariffPlanId = 4,
                    Price = 8700
                },
                new CategoryTariff
                {
                    Id = 21,
                    CategoryId = 6,
                    TariffPlanId = 1,
                    Price = 6300
                },
                new CategoryTariff
                {
                    Id = 22,
                    CategoryId = 6,
                    TariffPlanId = 2,
                    Price = 7400
                },
                new CategoryTariff
                {
                    Id = 23,
                    CategoryId = 6,
                    TariffPlanId = 3,
                    Price = 8500
                },
                new CategoryTariff
                {
                    Id = 24,
                    CategoryId = 6,
                    TariffPlanId = 4,
                    Price = 9800
                });


            
        }
    }
}
