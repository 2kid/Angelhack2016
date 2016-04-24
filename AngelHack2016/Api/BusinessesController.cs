using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngelHack2016.Models;

namespace AngelHack2016.Api
{
    public class BusinessesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Businesses
        public string GetBusinesses(string cuisine)
        {
            int[] primarykeys = db.Businesses.Where(r=>r.cuisine.Contains(cuisine)).Select(u=>u.BusinessId).ToArray();
            //return primary keys of Businesses
            //return primarykeys;
            //string s = string.Join(',',primarykeys);
            //return string.Format("{0}://{1}{2}", Request.RequestUri.Scheme,
            //Request.RequestUri.Host, "/Home/Maps",primarykeys);
            return Url.Link("Default", new { controller = "Home", action = "Maps" }) + "?" + string.Join("&", primarykeys.Select(x => "primaryKeys=" + x));
          
        }

        // GET: api/Businesses/5
        //[ResponseType(typeof(Business))]
        //public IHttpActionResult GetBusiness(int id)
        //{
        //    Business business = db.Businesses.Find(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(business);
        //}

        // PUT: api/Businesses/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutBusiness(int id, Business business)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != business.BusinessId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(business).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BusinessExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Businesses
        //[ResponseType(typeof(Business))]
        //public IHttpActionResult PostBusiness(Business business)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Businesses.Add(business);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = business.BusinessId }, business);
        //}

        // DELETE: api/Businesses/5
        //[ResponseType(typeof(Business))]
        //public IHttpActionResult DeleteBusiness(int id)
        //{
        //    Business business = db.Businesses.Find(id);
        //    if (business == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Businesses.Remove(business);
        //    db.SaveChanges();

        //    return Ok(business);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusinessExists(int id)
        {
            return db.Businesses.Count(e => e.BusinessId == id) > 0;
        }
    }
}