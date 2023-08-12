using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using HotelAdmin.Service.EmailService;
using System.Drawing;

namespace HotelAdmin.Service.OrderService
{
    public class DbDaoOrderPay : IDaoOrderPay
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public DbDaoOrderPay(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task DeleteConfirmedAsync(Guid id)
        {
            var order = await _context.OrderPays.FindAsync(id);
            if (order != null)
            {
                _context.OrderPays.Remove(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<OrderPay> GetAsync(Guid id)
        {
            await _context.Clients.LoadAsync();
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var order = await _context.OrderPays
                .SingleOrDefaultAsync(m => m.Id == id);

            return order;
        }

        public async Task<List<OrderPay>> IndexAsync(int page)
        {
            await _context.Clients.LoadAsync();
            var orders = await _context.OrderPays.ToListAsync();
            List<OrderPay> list = new List<OrderPay>();
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

        public async Task<bool> UpdateAsync(OrderPay order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BookingSendAsync(Guid id)
        {
            //await _context.Clients.LoadAsync();
            OrderPay order = await GetAsync(id);
            /*Subscriber personContains = _context.Subscribers.FirstOrDefault(x => x.Email == person.Email);
            if (personContains == null)
            {
                person.Date = DateTime.Now;
                _context.Subscribers.AddAsync(person);
                await _context.SaveChangesAsync();*/
                StringBuilder stringBuilder = new StringBuilder();
            /*stringBuilder.Append("<h2>Подтверждение бронирования отеля!</h2>");
            stringBuilder.Append("<div class=\"container\">");
            //stringBuilder.AppendFormat("container");
            stringBuilder.Append("<div class=\"row\">");
            //stringBuilder.AppendFormat("row");
            stringBuilder.Append("<div class=\"card\" style=\"margin-top:45px\">");
            //stringBuilder.AppendFormat("card");
            stringBuilder.Append("<div class=\"card-header auth-head\">");
            stringBuilder.Append("<h5 class=\"card-title\">");
            stringBuilder.Append(order.Client.Name);
            stringBuilder.Append("</h5>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("<div class=\"card-body auth\">");
            stringBuilder.Append("<h5>");
            stringBuilder.Append(order.Client.Name);
            stringBuilder.Append("</h5>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");*/

            /*string loginForm = $@"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>METANIT.COM</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>{order.Client.Name}</label><br />
                <input name='email' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
            stringBuilder.Append(loginForm);*/

            /*string BookingForm = $@"<!DOCTYPE html>
    <html lang='en'>
<head>
    <meta charset='utf-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <title>Login</title>
    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-4bw+/aepP/YC94hEpVNVgiZdgIC5+VKNBQNGCHeKRQN+PtmoHDEXuppvnDJzQIu9' crossorigin='anonymous'>
    <link rel='stylesheet' href='~/lib/bootstrap/dist/css/bootstrap.min.css' />
    <link rel='stylesheet' href='~/css/site.css' asp-append-version='true' />
    <link rel='stylesheet' href='~/HotelAdmin.styles.css' asp-append-version='true' />
</head>
<body class='body-auth' style='background-color:deepskyblue;'>
    <div class='container' style='background-color:deepskyblue;'>
        <div class='row'>
            <div class='col-md-6 col-md-offset-3' style='width:70%;'>
                <div class='card' style='background-color:snow;'>
                    <div class='card-header' style='background-color:deepskyblue;'>
                        <h5 class='card-title'> Login with Email and Password</h5>
                    </div>
                    <div class='card-body auth' style='background-color:powderblue;'>
                        <form asp-action='Login' asp-controller='Access' method='post'>
                            <div class='form-group'>
                                <label class='control-label'>{order.Client.Name}</label>
                                <input required asp-for='Email' type='email' placeholder='Email' class='form-control' />
                            </div>
                            <div class='form-group'>
                                <label class='control-label'>{order.Client.Name}</label>
                                <input required asp-for='Password' type='password' placeholder='Password' class='form-control' />
                            </div>
                            <br/>
                            <div>
                                <button type='submit' class='btn btn-secondary'>Login</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js' integrity='sha384-HwwvtgBNo3bZJJLYd8oVXjrBZt8cqVSpeBNS5n7C8IVInixGAoxmnlMuBnhbgrkm' crossorigin='anonymous'></script>
    <script src='~/lib/jquery/dist/jquery.min.js'></script>
    <script src='~/lib/bootstrap/dist/js/bootstrap.bundle.min.js'></script>
    <script src='~/js/site.js' asp-append-version='true'></script>
</body>

</html>";*/

            /*string BookingForm = $@"
            <div style='width:70%;' >
                <div style='background-color:deepskyblue;'>
                    <h3>Поздравляем, {order.Client.Name}! Ваше бронирование подтверждено</h3>
                       <div style='background-color:snow;'>
                          <h3>My Hotel адрес</h3>
                             <div style='background-color:powderblue;'>
                                 <dl class=""row"">
                                    <dt class=""col-sm-4 info-sise"" style='width:35%;'>
                                        Номер заказа:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"" style='width:35%;'>
                                        {order.Id}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"" style='width:35%;'>
                                        Дата заказа:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"" style='width:35%;'>
                                        {order.Date}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Количество гостей:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.GuestCount}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Имя клиента:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Name}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Фамилия клиента:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Surname}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Почта:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Email}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Телефон:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Phone}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Дата заезда:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Start}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Дата выезда:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.End}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Номер комнаты:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Room.Number}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Категория:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Room.Category.Name}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Тариф:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Tariff.TariffPlan.Description}
                                    </dd>
                                    <dt class=""col-sm-4 info-sise"">
                                        Цена:
                                    </dt>
                                    <dd class=""col-sm-8 info-sise"">
                                        {order.Client.Tariff.Price}
                                    </dd>
                                 </dl>
                             </div>
                   </div>
                </div>
            </div>";*/

            string BookingForm = $@"
            <div style='width:70%;' >
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
                            <h3>Номер бронирования:&ensp; {order.Id}</h3>
                            <table>
                               <tbody>
                                   <tr>
                                       <td>Номер:</td>
                                       <td>{order.Client.Room.Category.PersonsCount}-х местный, {order.Client.Room.Category.Name}, {order.Client.Room.Category.Square}</td>
                                   </tr>
                                   <tr>
                                       <td>Режим питания:</td>
                                       <td>{order.Client.Tariff.TariffPlan.Description}</td>
                                   </tr>
                                   <tr>
                                       <td>Количество гостей:</td>
                                       <td>{order.GuestCount}</td>
                                   </tr>
                                   <tr>
                                       <td>Количество ночей:</td>
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
                           <h3><span style=""font-size: 20px"">Стоимость проживания:&ensp;</span>{order.Client.Tariff.Price} Р</h3>
                           <h3>Оплачено:&ensp;{order.Client.Tariff.Price} Р</h3>
                        </div>
                    </div>
                </div>
            </div>";
            stringBuilder.Append(BookingForm);



            await _emailSender.SendEmailAsync(order.Client.Email, "Подтверждение бронирования", stringBuilder.ToString());
                return true;
            /*}
            else return false;*/
            /*_context.Update(order);
            await _context.SaveChangesAsync();*/
            //return true;
        }
    }
}
