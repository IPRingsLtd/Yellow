using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using _360Appraisal.Models;
using System.Threading.Tasks;

namespace _360Appraisal.Controllers
{
    public class InitController : Controller
    {
        public async Task<List<Question>> GetQuestions(AppContext db)
        {
            var TotalNoOfQuestion = await db.Questions.CountAsync();
            var NoOfQuestion = TotalNoOfQuestion < 10 ? TotalNoOfQuestion : 10;
            var SelectedQuestions = new List<Question>();
            var SelectedQuestionKeys = new List<string>();

            var Sections = await db.Sections.Include(x => x.Topics).ToListAsync();

            while (SelectedQuestions.Count < NoOfQuestion)
            {
                foreach (var Section in Sections)
                {
                    foreach (var Topic in Section.Topics)
                    {
                        var Question = await db.Questions.Where(x => x.Topic.Key == Topic.Key && !SelectedQuestionKeys.Contains(x.Key)).OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();

                        if (Question != null)
                        {
                            SelectedQuestionKeys.Add(Question.Key);
                            SelectedQuestions.Add(Question);
                        }
                    }
                }
            }

            return SelectedQuestions;

        }

        public static string GetCurrentFinancialYear()
        {
            var d = DateTime.Today;
            return (d.Month < 5) ? (d.Year - 1) + "-" + (d.Year) : (d.Year) + "-" + (d.Year - 1);
        }


        public async Task<ActionResult> Index()
        {
            try
            {
                using (AppContext db = new AppContext())
                {
                    foreach (var User in (await Service.List()).Take(2))
                    {
                        var Feedback = new Feedback
                        {
                            Key = Guid.NewGuid().ToString(),
                            UserID = User.code,
                            ReviewerID = User.code,
                            Type = FeedbackTypes.Self,
                            FinancialYear = GetCurrentFinancialYear()
                        };

                        db.Feedbacks.Add(Feedback);

                        foreach (var Question in await GetQuestions(db))
                        {
                            db.Scores.Add(new Score
                            {
                                Feedback = Feedback,
                                Question = Question
                            });
                        }
                    }

                    await db.SaveChangesAsync();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }
    }
}