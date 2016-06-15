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
using Newtonsoft.Json.Linq;

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
            var id = "\"" + User.Identity.GetUserId() + "\"";
            var useridwithoutQ = User.Identity.GetUserId();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var questions = db.Questions.Where(u => u.Id == id).Select(a => new SelectListGroup
            //{
            //    Name = a.Question,
            //    Disabled = a.isRelevant
            //})            

            // var userTags = db.UsersData.Where(a => a.UserId == id).Select(a => a).ToList<UsersData>();

            var userdata = db.UsersData.Where(a => a.UserId == id);

            var usertags = db.UserTags.Where(a => a.UserId == useridwithoutQ).Select(a => a.Tags).ToString();

            var expertise = userdata.Select(a => a.ExpertiseLevel).ToString();

            var splitTags = usertags.Split(':');

            List<PostWithDifficulty> postdetails = new List<PostWithDifficulty>();
            switch (expertise) {                
                case "1":
                    postdetails = db.PostDetails.Join(db.Posts, a=> a.PostId, b=> b.PostId, (a,b) => new PostWithDifficulty{ Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty == "Easy").ToList();
                    break;
                case "2":
                    postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty == "Moderate").ToList();
                    break;
                case "3":
                    postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty == "Hard").ToList();
                    break;
            }
            List<String> questions = new List<String>();
            
            foreach (var item in splitTags)
            {
                foreach (var i in postdetails)
                {
                    if (i.Tags.ToLower().Contains(item.ToLower())){
                        var json = JObject.Parse(i.Question);
                        questions.Add(json.GetValue("Body").ToString());
                    }
                }

                if (questions.Count == 10)
                    break;             
                    
            }
             
            //if (userTags.Count == 0)
            //{
                
            //    return HttpNotFound();
            //}
            return PartialView("View",questions);
        }
    }
}
