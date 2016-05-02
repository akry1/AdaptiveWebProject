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
        //[OutputCache(Duration = 600, SqlDependency = "db:PostDetails")]
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
            List<String> questions = new List<String>();

            try {
                var id = User.Identity.GetUserId();

                var userdata = db.UsersData.Where(a => a.UserId.Contains(id));

                var username = db.UserName.Where(a => a.UserId.Contains(id)).Select(a => a.Name).ToList<String>();

                var usertags = db.UserTags.Where(a => a.UserId.Contains(id)).Select(a => a.Tags).ToList<String>();

                var expertise = userdata.Select(a => a.Points).ToList();

                var points = userdata.Select(a => a.ExpertiseLevel).ToList();

                var upvotes = db.UserUpvotes.Where(a => a.UserId.Contains(id)).Select(a => a.Upvotes).ToList<String>();

                var acceptedAnswers = db.UserAcceptedAnswers.Where(a => a.UserId.Contains(id)).Select(a => a.AcceptedAnswers).ToList<String>();

                var splitTags = usertags[0].Trim().Split(':');



                List<PostWithDifficulty> postdetails = new List<PostWithDifficulty>();
                string userExpertise = "";
                int maxPoints = 0,pointsMod = 0;
                switch (expertise[0])
                {
                    case 1:
                        userExpertise = "Naive";
                        //minPoints = 0;
                        maxPoints = 100;
                        pointsMod = points[0];
                        postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Easy")).ToList();
                        break;
                    case 2:
                        userExpertise = "Mediocre";
                        maxPoints = 400;
                        pointsMod = points[0] - 100;
                        postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Moderate")).ToList();
                        break;
                    case 3:
                        userExpertise = "Expert";
                        maxPoints = 10000;
                        if (points[0] > 10000)
                            pointsMod = 10000;
                        else
                            pointsMod = points[0] - 500;
                        postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Hard")).ToList();
                        break;
                }
                

                //JavaScriptSerializer js = new JavaScriptSerializer();           

                ViewBag.Name = username[0].Replace("\"", "");
                ViewBag.Points = points[0];
                ViewBag.Accepted = Int64.Parse(Regex.Replace(acceptedAnswers[0],"\\D",""));
                ViewBag.Expertise = userExpertise;
                ViewBag.Tags = splitTags;
                ViewBag.Upvotes = Int64.Parse(Regex.Replace(upvotes[0], "\\D", ""));
                ViewBag.PointsMod = pointsMod;
                ViewBag.MaxPoints = maxPoints;
                

                foreach (var item in splitTags)
                {
                    Boolean flag = false;
                    foreach (var i in postdetails)
                    {
                        if (i.Tags.ToLower().Contains(item.ToLower())) {
                            //var json = JObject.Parse(i.Question.Trim());
                            //QuestionParts qp = (QuestionParts)js.DeserializeObject(i.Question.Trim());
                            var q = Regex.Match(i.Question.Trim(), "[^ ]*'Body':(.*), 'Title'").Groups[1].Value;
                            q = Regex.Replace(q, "['\"]", "");
                            q = Regex.Replace(q, "(\\\\n)+", "<br />");
                            q = Regex.Replace(q, "(\\\\r)+", "<br />");
                            //questions.Add(json.GetValue("Body").ToString());
                            questions.Add(q);
                        }

                        if (questions.Count == 10)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag) break;

                }
            }
            catch(Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View(questions);
        }

    }
}