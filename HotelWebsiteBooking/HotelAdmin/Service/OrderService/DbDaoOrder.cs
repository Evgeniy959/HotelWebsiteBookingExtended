using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.BookingService;
using HotelAdmin.Service.EmailService;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HotelAdmin.Service.OrderService
{
    public class DbDaoOrder : IDaoOrder
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IDaoBooking _daoBooking;

        public DbDaoOrder(AppDbContext context, IEmailSender emailSender, IDaoBooking daoBooking)
        {
            _context = context;
            _emailSender = emailSender;
            _daoBooking = daoBooking;
        }
        public async Task DeleteConfirmedAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            await _context.Clients.LoadAsync();
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);

            return order;
        }

        public async Task<List<Order>> IndexAsync(int page)
        {
            await _context.Clients.LoadAsync();
            var orders = await _context.Orders.ToListAsync();
            List<Order> list = new List<Order>();
            int TotalPages = (int)Math.Ceiling(orders.Count / 10.0);

            if (!orders.Any())
            {
                return orders;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < orders.Count; i++)
                {
                    list.Add(orders[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(orders[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BookingSendAsync(Guid id)
        {
            try
            {
                Order order = await GetAsync(id);
                Booking booking = await _daoBooking.AddBookingAsync();
                StringBuilder stringBuilder = new StringBuilder();
                string BookingForm = $@"
                <div style='width:75%;' >
                    <div style='background-color:deepskyblue; padding: 10px; box-sizing: border-box;'>
                    <h3 style='color:white;'>Поздравляем, {order.Client.Name}! Ваше бронирование подтверждено</h3>
                        <div style='background-color:snow; padding: 10px; box-sizing: border-box;'>
                        <h3 style='color:deepskyblue;'>MyHotel A Premium</h3>
                        <table>
                           <tbody>
                               <tr>
                                   <td>Адрес:</td>
                                   <td>Krasnodar territory, Sochi, Lenin str., 2, a/z 110</td>
                               </tr>
                               <tr>
                                   <td>Телефон:&ensp;</td>
                                   <td>8(800) 350 4590</td>
                               </tr>
                               <tr>
                                   <td>Эл.почта:&ensp;</td>
                                   <td>info.myhotel@gmail.com</td>
                               </tr>
                           </tbody>
                        </table>
                            <div style='background-color:powderblue; padding: 10px; box-sizing: border-box; margin: 10px;'>
                                <h3>Номер бронирования:&ensp; {booking.Number}</h3>
                                <table>
                                   <tbody>
                                       <tr>
                                           <td>Номер:</td>
                                           <td>{order.Client.Room.Category.PersonsCount}-х местный, {order.Client.Room.Category.Name}, {order.Client.Room.Category.Square}</td>
                                       </tr>
                                       <tr>
                                           <td>Тариф:</td>
                                           <td>{order.Client.Tariff.TariffPlan.Description}</td>
                                       </tr>
                                       <tr>
                                           <td>Число гостей:</td>
                                           <td>{order.GuestCount}</td>
                                       </tr>
                                       <tr>
                                           <td>Число ночей:</td>
                                           <td>{order.Client.End.Subtract(order.Client.Start).Days}</td>
                                       </tr>
                                       <tr>
                                           <td>Дата заезда:</td>
                                           <td>{order.Client.Start.ToLongDateString()} с 12:00</td>
                                       </tr>
                                       <tr>
                                           <td>Дата выезда:</td>
                                           <td>{order.Client.End.ToLongDateString()} до 12:00</td>
                                       </tr>                                                                                
                                       <tr>
                                           <td>Бронь на имя:</td>
                                           <td>{order.Client.Name} {order.Client.Surname}</td>
                                       </tr>
                                       <tr>
                                           <td>Телефон:</td>
                                           <td>{order.Client.Phone}</td>
                                       </tr>
                                       <tr>
                                           <td>Email адрес:</td>
                                           <td>{order.Client.Email}</td>
                                       </tr>
                                   </tbody>
                               </table>
                               <br/>
                               <h3><span style=""font-size: 20px"">Стоимость проживания:&ensp;</span>{order.Client.Tariff.Price*(order.Client.End.Subtract(order.Client.Start)).Days} Р</h3>
                               <h3>Оплатить при заселении</h3>
                            </div>
                        </div>
                    </div>
                </div>";
                stringBuilder.Append(BookingForm);
                await _emailSender.SendEmailAsync(order.Client.Email, "Подтверждение бронирования", stringBuilder.ToString());
                order.Status = "отправлено";
                order.BookingNumber = booking.Number;
                order.Date = booking.Date;
                await UpdateAsync(order);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
