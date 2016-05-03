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
using System.Data.Entity;

namespace AdaptiveWebProject.Controllers
{
    
    public class HomeController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        AppDBEntities dbView = new AppDBEntities();

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
        [OutputCache(Duration = 100, VaryByParam = "none")]

        public ActionResult Dashboard()
        {
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
                        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Easy")).ToList();
                        postdetails = dbView.EasyPosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                        break;
                    case 2:
                        userExpertise = "Mediocre";
                        maxPoints = 400;
                        pointsMod = points[0] - 100;
                        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Moderate")).ToList();
                        postdetails = dbView.ModeratePosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                        break;
                    case 3:
                        userExpertise = "Expert";
                        maxPoints = 10000;
                        if (points[0] > 10000)
                            pointsMod = 10000;
                        else
                            pointsMod = points[0] - 500;
                        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Hard")).ToList();
                        postdetails = dbView.HardPosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                        break;
                }
                foreach (var item in splitTags)
                {
                    Boolean flag = false;
                    foreach (var i in postdetails)
                    {
                        if (i.Tags.ToLower().Contains(item.ToLower()))
                        {
                            var q = Regex.Match(i.Question.Trim(), "[^ ]*'Body':(.*), 'Title'").Groups[1].Value;
                            q = Regex.Replace(q, "['\"]", "");
                            q = Regex.Replace(q, "((\\\\r)+\\\\n)+", "<br />");
                            //q = Regex.Replace(q, "(\\\\r)+", "");
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

                ViewBag.Name = username[0].Replace("\"", "");
                ViewBag.Points = points[0];
                ViewBag.Accepted = Int64.Parse(Regex.Replace(acceptedAnswers[0], "\\D", ""));
                ViewBag.Expertise = userExpertise;
                ViewBag.Tags = splitTags;
                ViewBag.Upvotes = Int64.Parse(Regex.Replace(upvotes[0], "\\D", ""));
                ViewBag.PointsMod = pointsMod;
                ViewBag.MaxPoints = maxPoints;
            }
            catch(Exception ex)
            {
                RedirectToAction("Dashboard", "Home");
            }
            return View(questions);
        }
        

    }
}