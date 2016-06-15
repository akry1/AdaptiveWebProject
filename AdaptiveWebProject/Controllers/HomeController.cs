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
            List<QuestionParts> questions = new List<QuestionParts>();

            try
            {
                var id = User.Identity.GetUserId();

                var userdata = db.UsersData.Where(a => a.UserId.Contains(id));

                var username = db.UserName.Where(a => a.UserId.Contains(id)).Select(a => a.Name).ToList<String>();

                var usertags = db.UserTags.Where(a => a.UserId.Contains(id)).Select(a => a.Tags).ToList<String>();

                var expertise = userdata.Select(a => a.Points).ToList();
               
                var points = userdata.Select(a => a.ExpertiseLevel).ToList();

                var upvotes = db.UserUpvotes.Where(a => a.UserId.Contains(id)).Select(a => a.Upvotes).ToList<String>();

                var acceptedAnswers = db.UserAcceptedAnswers.Where(a => a.UserId.Contains(id)).Select(a => a.AcceptedAnswers).ToList<String>();

                var splitTags = usertags[0].Trim().Split(':');

                var experts = db.UsersData.Join(db.UserTags, a => a.UserId, b => b.UserId, (a, b) => new { Tags = b.Tags.ToLower(), UserId = a.UserId, Points = a.ExpertiseLevel, ExpertiseLevel = a.Points }).Where(a => a.Points == 3).ToList();

                List<PostWithDifficulty> postdetails = new List<PostWithDifficulty>();
                string userExpertise = "";
                int maxPoints = 0, pointsMod = 0;

                //switch (expertise[0])
                //{
                //    case 1:
                //        userExpertise = "Naive";
                //        //minPoints = 0;
                //        maxPoints = 100;
                //        pointsMod = points[0];
                //        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Easy")).ToList();
                //        postdetails = dbView.EasyPosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                //        break;
                //    case 2:
                //        userExpertise = "Mediocre";
                //        maxPoints = 400;
                //        pointsMod = points[0] - 100;
                //        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Moderate")).ToList();
                //        postdetails = dbView.ModeratePosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                //        break;
                //    case 3:
                //        userExpertise = "Expert";
                //        maxPoints = 10000;
                //        if (points[0] > 10000)
                //            pointsMod = 10000;
                //        else
                //            pointsMod = points[0] - 500;
                //        //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Hard")).ToList();
                //        postdetails = dbView.HardPosts.Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).ToList();
                //        break;
                //}
                //foreach (var item in splitTags)
                //{
                //    Boolean flag = false;
                //    foreach (var i in postdetails)
                //    {
                //        if (i.Tags.ToLower().Contains(item.ToLower()))
                //        {
                //            var q = Regex.Match(i.Question.Trim(), "[^ ]*'Body':(.*), 'Title'").Groups[1].Value;
                //            q = Regex.Replace(q, "['\"]", "");
                //            q = Regex.Replace(q, "((\\\\r)+\\\\n)+", "<br />");
                //            //q = Regex.Replace(q, "(\\\\r)+", "");
                //            questions.Add(q);
                //        }

                //        if (questions.Count == 10)
                //        {
                //            flag = true;
                //            break;
                //        }
                //    }

                //    if (flag) break;

                //}


                foreach (var item in splitTags)
                {
                    switch (expertise[0])
                    {
                        case 1:
                            userExpertise = "Naive";
                            //minPoints = 0;
                            maxPoints = 100;
                            pointsMod = points[0];
                            //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Easy")).ToList();
                            postdetails = dbView.EasyPosts.Where(a => a.Tags.ToLower().Contains(item.ToLower())).Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).Take(10).ToList();
                            break;
                        case 2:
                            userExpertise = "Mediocre";
                            maxPoints = 400;
                            pointsMod = points[0] - 100;
                            //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Moderate")).ToList();
                            postdetails = dbView.ModeratePosts.Where(a => a.Tags.ToLower().Contains(item.ToLower())).Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).Take(10).ToList();
                            break;
                        case 3:
                            userExpertise = "Expert";
                            maxPoints = 10000;
                            if (points[0] > 10000)
                                pointsMod = 10000;
                            else
                                pointsMod = points[0] - 500;
                            //postdetails = db.PostDetails.Join(db.Posts, a => a.PostId, b => b.PostId, (a, b) => new PostWithDifficulty { Difficulty = a.Difficulty, Tags = b.Tags, Question = b.Question }).Where(a => a.Difficulty.Contains("Hard")).ToList();
                            postdetails = dbView.HardPosts.Where(a => a.Tags.ToLower().Contains(item.ToLower())).Select(a => new PostWithDifficulty { PostId = a.PostId, Tags = a.Tags, Question = a.Question }).Take(10).ToList();
                            break;
                    }

                    foreach (var i in postdetails)
                    {
                        QuestionParts question = new QuestionParts();
                        ForceDirectedJson f = new ForceDirectedJson();
                        var q = Regex.Match(i.Question.Trim(), "[^ ]*'Body':(.*), 'Title'").Groups[1].Value;
                        q = Regex.Replace(q, "['\"]", "");
                        q = Regex.Replace(q, "((\\\\r)+\\\\n)+", "<br />");
                        //q = Regex.Replace(q, "(\\\\r)+", "");
                        question.Body = q;

                        question.PostId = i.PostId;                       

                        // For Force directed json
                        var postTags = i.Tags.ToLower().Split(':');

                        for (int j = 1; j < 4; j++)
                        {
                            f.Postid = i.PostId;
                            f.Tags[j-1] = postTags[j];
                            if (j == 1)
                                //f.list1 = experts.Where(a => a.Tags.Contains(postTags[j].ToLower())).Select(a => a.UserId).Take(5).ToArray<string>();
                                f.list1 = db.Database.SqlQuery<ExpertUsers>(@"SELECT top 5 b.ExpertiseLevel, a.UserId FROM dbo.UsersDatas b, dbo.UserTags a WHERE (b.Points = 3) and b.UserId like concat('%', a.UserId, '%') and a.Tags like '%" + postTags[j] + @"%'").ToList<ExpertUsers>();
                            else if (j == 2)
                                f.list2 = db.Database.SqlQuery<ExpertUsers>(@"SELECT top 5 b.ExpertiseLevel, a.UserId FROM dbo.UsersDatas b, dbo.UserTags a WHERE (b.Points = 3) and b.UserId like concat('%', a.UserId, '%') and a.Tags like '%" + postTags[j] + @"%'").ToList<ExpertUsers>();
                            else if (j == 3)
                                f.list2 = db.Database.SqlQuery<ExpertUsers>(@"SELECT top 5 b.ExpertiseLevel, a.UserId FROM dbo.UsersDatas b, dbo.UserTags a WHERE (b.Points = 3) and b.UserId like concat('%', a.UserId, '%') and a.Tags like '%" + postTags[j] + @"%'").ToList<ExpertUsers>();
                        }
                        question.force = f;

                        questions.Add(question);

                        if (questions.Count == 10)
                        {
                            break;
                        }
                    }

                    if (questions.Count == 10) break;
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
            catch (Exception ex)
            {
                RedirectToAction("Dashboard", "Home");
            }
            return View(questions);
        }

        public ActionResult ForceDirected(int id)
        {
            ForceDirectedJson result = new ForceDirectedJson();
            result.Postid = id;
            var postTags = db.Posts.Where(a => a.PostId == id).Select(a => a.Tags).ToList();

            var splitTags = postTags[0].Split(':');

            var experts = db.UsersData.Where(a => a.Points == 3).Join(db.UserTags, a => a.UserId, b => b.UserId, (a, b) => new { Tags = b.Tags.ToLower(), UserId = a.UserId }).ToList();

            for (int i = 1; i < splitTags.Length; i++)
            {
                result.Tags[i] = splitTags[i];
                //result.UserIds[i] =  experts.Where(a => a.Tags.Contains(splitTags[i].ToLower())).Select(a=>a.UserId).ToList();
            }

            return Json(result);
        }


    }
}