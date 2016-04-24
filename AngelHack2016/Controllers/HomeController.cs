using AngelHack2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AngelHack2016.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FeedbackStart()
        {
            return View();
        }
       
        public ActionResult Maps(string[] primaryKeys)
        {
            List<lnglat> list = new List<lnglat>();
            foreach(string value in primaryKeys)
            {
                int id = int.Parse(value);
                Business business = db.Businesses.Find(id);
                list.Add(new lnglat() { lat = business.latitude, lng = business.longitude , BusinessName = business.Name});
            }
            return View(list);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string referenceNo, int transactionNo)
        {
           Business business = db.Businesses.Where(r => r.referenceNo == referenceNo).SingleOrDefault();
            if (business.BusinessId != 0)
            {
                bool isUsedTransaction = db.FeedBacks.Any(a => a.transactionNo == transactionNo);
                if (!isUsedTransaction)
                {
                    //Feedback feedback = new Feedback();
                    //feedback.BusinessId = businessId;
                    //feedback.transactionNo = transactionNo;
                    //db.FeedBacks.Add(feedback);
                    //db.SaveChanges();
                    if (transactionNo >= business.minTransactionNumber && transactionNo <= business.maxTransactionNumber)
                    {
                        return RedirectToAction("FeedbackForm", new { businessId = business.BusinessId, transactionNo = transactionNo });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Transaction Number");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This transaction Number has been claimed already");
                    return View();
                }
            }
            ModelState.AddModelError("", "Invalid reference Number");
            return View();
        }


        public ActionResult FeedbackForm(int businessId,int transactionNo)
        {
                Feedback feedback = new Feedback();
                    feedback.BusinessId = businessId;
                    feedback.transactionNo = transactionNo;
                   
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeedbackForm(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                if (!db.FeedBacks.Any(a => a.transactionNo == feedback.transactionNo))
                {
                    feedback.DateTimeSubmitted = DateTime.Now;
                    db.FeedBacks.Add(feedback);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
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
    }
}