using MVCReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCReview.Controllers
{
    public class ScheduleController : Controller
    {
        private DB_128040_practiceEntities db = new DB_128040_practiceEntities();

        // GET: Schedule
        public ActionResult Index(int? year)
        {
            if(year==null)
            {
                year = DateTime.Now.Year;
            }
            var games = db.FootballSchedules.Where(x=> x.Season==year.Value);

            return View(games);
        }
    }
}