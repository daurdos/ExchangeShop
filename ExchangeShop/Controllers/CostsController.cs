using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExchangeShop.Models;

namespace ExchangeShop.Controllers
{
    public class CostsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Costs
        public ActionResult Index()
        {
            var costs = db.Costs.Include(c => c.Currency).Include(c => c.Shop);
            return View(costs.ToList());
        }

        // GET: Costs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost cost = db.Costs.Find(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            return View(cost);
        }

        // GET: Costs/Create
        public ActionResult Create()
        {
            var model = new Cost { Date = DateTime.Now }; // перекидываю текущую дату по умлочанию
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "Name");
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "Name");
            return View(model);
        }

        // POST: Costs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Date,CurrencyId,ShopId")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                db.Costs.Add(cost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "Name", cost.CurrencyId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "Name", cost.ShopId);
            return View(cost);
        }

        // GET: Costs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost cost = db.Costs.Find(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "Name", cost.CurrencyId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "Name", cost.ShopId);
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Date,CurrencyId,ShopId")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "Name", cost.CurrencyId);
            ViewBag.ShopId = new SelectList(db.Shops, "Id", "Name", cost.ShopId);
            return View(cost);
        }

        // GET: Costs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cost cost = db.Costs.Find(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cost cost = db.Costs.Find(id);
            db.Costs.Remove(cost);
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






        // linq для фильтрации стоимости, в том числе позволяет получить последнюю стоимость относящуюся к определенному обменному пункту
        public ActionResult GetSortedCosts(int? id)
        {
            var costs = db.Costs.Include(c => c.Currency).Include(e => e.Shop);
            List<Cost> costList = costs.ToList();
            var justByDate = costs.OrderByDescending(c => c.Date);
            var byShops = from c in costs
                          where c.ShopId == id
                          select c;
            var LastDate = from n in costs
                           group n by n.ShopId into g
                           select g.OrderByDescending(t => t.Date).FirstOrDefault();

            var LastDateByShop = from n in costs
                                 where n.ShopId == id
                                 group n by n.ShopId into g
                                 select g.OrderByDescending(t => t.Date).FirstOrDefault();

            //List<Cost> costList = db.Costs.ToList();
            return View(LastDateByShop);

        }






    }
}
