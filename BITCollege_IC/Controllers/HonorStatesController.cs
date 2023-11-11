using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BITCollege_IC.Data;
using BITCollege_IC.Models;

namespace BITCollege_IC.Controllers
{
    public class HonorStatesController : Controller
    {
        private BITCollege_ICContext db = new BITCollege_ICContext();

        // GET: HonorStates
        public ActionResult Index()
        {
            return View(HonorState.GetInstance());
        }

        // GET: HonorStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorState honorState = (HonorState)db.GradePointStates.Find(id);
            if (honorState == null)
            {
                return HttpNotFound();
            }
            return View(honorState);
        }

        // GET: HonorStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HonorStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] HonorState honorState)
        {
            if (ModelState.IsValid)
            {
                db.GradePointStates.Add(honorState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(honorState);
        }

        // GET: HonorStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorState honorState = (HonorState)db.GradePointStates.Find(id);
            if (honorState == null)
            {
                return HttpNotFound();
            }
            return View(honorState);
        }

        // POST: HonorStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradePointStateId,LowerLimit,UpperLimit,TuitionRateFactor")] HonorState honorState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(honorState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(honorState);
        }

        // GET: HonorStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorState honorState = (HonorState)db.GradePointStates.Find(id);
            if (honorState == null)
            {
                return HttpNotFound();
            }
            return View(honorState);
        }

        // POST: HonorStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HonorState honorState = (HonorState)db.GradePointStates.Find(id);
            db.GradePointStates.Remove(honorState);
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
