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
        
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(string name, string email, string content, int? idR, int? rId)
        {
            _comment.AddCommentAsync(name, email, content);
            //return Redirect("/Home/Standard");
            //return Redirect("/Home/Index");
            return RedirectToAction("Standard", "Home", new { idRoom = idR, roomId = rId });
            /*try
            {
                _comment.AddCommentAsync(name, email, content);
                return Redirect("/Home/Index");
                //throw new Exception();
                //return RedirectToAction(nameof(Index));                
            }
            catch
            //catch(Exception)
            {
                //return Redirect("/Home/Index");
                return View();
            }*/
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
