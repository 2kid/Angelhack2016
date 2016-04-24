using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngelHack2016.Models;
using System.Threading.Tasks;

namespace AngelHack2016.Controllers
{
    [Authorize(Roles="Business")]
    public class BusinessesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.Where(r=>r.Username == User.Identity.Name).ToList());
        }

        // GET: Businesses
        public async Task<ActionResult> FeedbackAnalysis(int businessId)
        {
            List<Feedback> unAnalyzedFeedback = db.FeedBacks.Include(a => a.Business).Where(r => r.Business.Username == User.Identity.Name && r.SentimentScore == null && r.BusinessId == businessId).ToList();
            List<int> oldAnalyzedFeedbackIds = db.FeedBacks.Include(a => a.Business).Where(r => r.Business.Username == User.Identity.Name && r.SentimentScore != null && r.BusinessId == businessId).Select(u => u.FeedbackId).ToList();
            unAnalyzedFeedback.RemoveAll(b => oldAnalyzedFeedbackIds.Contains(b.FeedbackId));
            if(unAnalyzedFeedback.Count > 0)
            {
                await Helper.MakeRequest(db, unAnalyzedFeedback);
            }

            List<Feedback> analyzedFeedback = db.FeedBacks.Include(a => a.Business).Where(r => r.Business.Username == User.Identity.Name && r.SentimentScore != null && r.BusinessId == businessId).ToList();

                FeedbackResult result = new FeedbackResult();
                result.PositiveSentiments = analyzedFeedback.Where(m => m.SentimentScore >= 0.6m).ToList();
                result.NegativeSentiments = analyzedFeedback.Where(m => m.SentimentScore <= 0.4m).ToList();
                result.NeutralSentiments = analyzedFeedback.Where(m => m.SentimentScore < 0.6m && m.SentimentScore > 0.4m).ToList();
            return View(result);
        }

        // GET: Businesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessId,Name,Industry,maxTransactionNumber,minTransactionNumber,referenceNo,longitude,latitude,cuisine")] Business business)
        {
            double a = business.longitude;
            double b = business.latitude;
            if (ModelState.IsValid)
            {
                business.Username = User.Identity.Name;
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: Businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessId,Name,Industry,maxTransactionNumber,minTransactionNumber,referenceNo,longitude,latitude,cuisine")] Business business)
        {
            if (ModelState.IsValid)
            {
                business.Username = User.Identity.Name;
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: Businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
