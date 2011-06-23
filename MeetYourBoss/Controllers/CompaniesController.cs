using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetYourBoss.Models;

namespace MeetYourBoss.Controllers
{ 
    public class CompaniesController : Controller
    {
        private MeetYourBossEntities db = new MeetYourBossEntities();

        //
        // GET: /Companies/

        public ViewResult Index()
        {
            var companies = db.CompaniesViews;
            return View(companies.ToList());
        }

        //
        // GET: /Companies/Details/5

        public ViewResult Details(int id)
        {
            Company companies = db.Companies.Single(c => c.id == id);
            return View(companies);
        }

        //
        // GET: /Companies/Create

        public ActionResult Create()
        {
            ViewBag.companysize_id = new SelectList(db.CompanySizes, "id", "descryption");
            return View();
        } 

        //
        // POST: /Companies/Create

        [HttpPost]
        public ActionResult Create(Company companies)
        {
            if (ModelState.IsValid)
            {
                db.Companies.AddObject(companies);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.companysize_id = new SelectList(db.CompanySizes, "id", "descryption", companies.companysize_id);
            return View(companies);
        }
        
        //
        // GET: /Companies/Edit/5
 
        public ActionResult Edit(int id)
        {
            Company companies = db.Companies.Single(c => c.id == id);
            ViewBag.companysize_id = new SelectList(db.CompanySizes, "id", "descryption", companies.companysize_id);
            return View(companies);
        }

        //
        // POST: /Companies/Edit/5

        [HttpPost]
        public ActionResult Edit(Company companies)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Attach(companies);
                db.ObjectStateManager.ChangeObjectState(companies, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companysize_id = new SelectList(db.CompanySizes, "id", "descryption", companies.companysize_id);
            return View(companies);
        }

        //
        // GET: /Companies/Delete/5
 
        public ActionResult Delete(int id)
        {
            Company companies = db.Companies.Single(c => c.id == id);
            return View(companies);
        }

        //
        // POST: /Companies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Company companies = db.Companies.Single(c => c.id == id);
            db.Companies.DeleteObject(companies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}