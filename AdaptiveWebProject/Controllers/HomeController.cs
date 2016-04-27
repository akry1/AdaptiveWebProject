using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AdaptiveWebProject.Models;

namespace AdaptiveWebProject.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            //var id = User.Identity.GetUserId();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var questions = db.Questions.Where(u => u.Id == id).Select(a => new SelectListGroup
            //{
            //    Name = a.Question,
            //    Disabled = a.isRelevant
            //});

            var userTags = db.PostTags.Join(db.PostDetails, a => a.PostId, b => b.PostId, (a, b) => new SelectListItem { Text = a.Tags, Value = b.Difficulty }).Take(10);

            if (userTags == null)
            {
                return HttpNotFound();
            }
            return View(userTags);
        }

    }
}