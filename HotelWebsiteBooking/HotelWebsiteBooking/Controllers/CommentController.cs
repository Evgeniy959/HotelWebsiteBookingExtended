using HotelWebsiteBooking.Service.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebsiteBooking.Controllers
{
    public class CommentController : Controller
    {
        private readonly IDaoComment _comment;
        
        public CommentController(IDaoComment comment) 
        {
            _comment = comment;
        }
        
        [HttpPost]
        public ActionResult CreateComment(string name, string email, string content, string title, int? idR, int? rId)
        {
            _comment.AddCommentAsync(name, email, content, title);
            if (title == "Standard") 
            {
                return RedirectToAction("Standard", "Home", new { idRoom = idR, roomId = rId });
            }
            if (title == "StandardBig")
            {
                return RedirectToAction("StandardBig", "Home", new { idRoom = idR, roomId = rId });
            }
            if (title == "StandardGood")
            {
                return RedirectToAction("StandardGood", "Home", new { idRoom = idR, roomId = rId });
            }
            if (title == "StandardGoodBig")
            {
                return RedirectToAction("StandardGoodBig", "Home", new { idRoom = idR, roomId = rId });
            }
            if (title == "SemiLuxury")
            {
                return RedirectToAction("SemiLuxury", "Home", new { idRoom = idR, roomId = rId });
            }
            if (title == "Luxury")
            {
                return RedirectToAction("Luxury", "Home", new { idRoom = idR, roomId = rId });
            }
            return RedirectToAction("Index", "Home");

        }

    }
}
