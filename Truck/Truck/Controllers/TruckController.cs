using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Truck.Models;
using GoogleAPI;

namespace Truck.Controllers
{
    public class TruckController : Controller
    {
        private FoodTruckContext db = new FoodTruckContext();
        
        // GET: /Truck/
        public ActionResult Index()
        {
            

            //var trucks = from t in db.Trucks select t;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    trucks = trucks.Where(s => s.TruckName.Contains(searchString));
            //}

            return View(db.Trucks.ToList());
            //List<Truck> ass = db.Trucks.ToList().Any(x=>x.ID.Equals())
        }

        // GET: /Truck/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodTruck foodtruck = db.Trucks.Find(id);
            if (foodtruck == null)
            {
                return HttpNotFound();
            }
            return View(foodtruck);
        }

        //// GET: /Truck/Create
        //[Authorize(Roles = "Admin, UserEdit, User")]
        //public ActionResult Create()
        //{
            
        //    return View();
        //}

        // POST: /Truck/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserEdit, User")]
        public ActionResult Create([Bind(Include="ID,TruckName,Location,StartTime,EndTime")] FoodTruck foodtruck)
        {
            if (ModelState.IsValid)
            {
                GeoResult result = GeoUtil.geoMapAddress(foodtruck.Location);
                foodtruck.Location = result.results[0].formatted_address.ToString();
                string lng = result.results[0].geometry.location.lng.ToString();
                string lat = result.results[0].geometry.location.lat.ToString();

                string mapURL = "http://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lng + "&zoom=15&size=800x800&sensor=false";
                foodtruck.Map = mapURL;
                db.Trucks.Add(foodtruck);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodtruck);
        }

        //// GET: /Truck/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FoodTruck foodtruck = db.Trucks.Find(id);
        //    if (foodtruck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(foodtruck);
        //}

        // POST: /Truck/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserEdit, User")]
        public ActionResult Edit([Bind(Include = "ID,TruckName,Location,StartTime,EndTime")] FoodTruck foodtruck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodtruck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodtruck);
        }

        //// GET: /Truck/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FoodTruck foodtruck = db.Trucks.Find(id);
        //    if (foodtruck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(foodtruck);
        //}

        // POST: /Truck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodTruck foodtruck = db.Trucks.Find(id);
            db.Trucks.Remove(foodtruck);
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
