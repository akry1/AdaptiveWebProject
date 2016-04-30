using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AdaptiveWebProject.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

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

            //var userTags = db.PostTags.Join(db.PostDetails, a => a.PostId, b => b.PostId, (a, b) => new SelectListItem { Text = a.Tags, Value = b.Difficulty }).Take(10);

            //if (userTags == null)
            //{
            //    return HttpNotFound();
            //}

            //var id = "\"" + User.Identity.GetUserId() + "\"";
            var id = User.Identity.GetUserId();

            var userdata = db.UsersData.Where(a => a.UserId.Contains(id));

            var usertags = db.UserTags.Where(a => a.UserId.Contains(id)).Select(a => a.Tags).ToList<String>();

            var expertise = userdata.Select(a => a.Points).ToList();

            var splitTags = usertags[0].Trim().Split(':');

            List<PostWithDifficulty> postdetails = new List<PostWithDifficulty>();
            switch (expertise[0])
            {
                case 1:
                    postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Easy")).ToList();
                    break;
                case 2:
                    postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains( "Moderate")).ToList();
                    break;
                case 3:
                    postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains( "Hard")).ToList();
                    break;
            }
            List<String> questions = new List<String>();

            //JavaScriptSerializer js = new JavaScriptSerializer();           
            

            foreach (var item in splitTags)
            {
                Boolean flag = false;
                foreach (var i in postdetails)
                {
                    if (i.Tags.ToLower().Contains(item.ToLower())){
                        //var json = JObject.Parse(i.Question);
                        //QuestionParts qp = (QuestionParts)js.DeserializeObject(i.Question.Trim());
                        var re = Regex.Match(i.Question.Trim(), "[^ ]*'Body':(.*), 'Title'").Groups[1].Value;
                        //questions.Add(json.GetValue("Body").ToString());
                        questions.Add(re);
                    }

                    if (questions.Count == 10)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag) break;

            }
            return View(questions);
        }

    }
}