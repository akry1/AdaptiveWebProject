using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdaptiveWebProject.Models;
using Microsoft.AspNet.Identity;

namespace AdaptiveWebProject.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Posts.Include(q => q.PostId);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
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

            var userTags = db.PostTags.Join(db.PostDetails, a => a.PostId, b => b.PostId, (a, b) => new SelectListItem{ Text = a.Tags, Value = b.Difficulty });

            if (userTags == null)
            {
                return HttpNotFound();
            }
            return View(userTags);
        }
    }
}
