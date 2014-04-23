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
    [Authorize(Roles = "Admin, Can Edit, User")]
    public class AdminController : Controller
    {
        private FoodTruckContext db = new FoodTruckContext();

        // GET: /Admin/
        public ActionResult Index()
        {
            return View(db.Trucks.ToList());
        }

        // GET: /Admin/Details/5
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

        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Admin/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,TruckName,Location,StartTime,EndTime")] FoodTruck foodtruck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodtruck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodtruck);
        }

        // GET: /Admin/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /Admin/Delete/5
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
