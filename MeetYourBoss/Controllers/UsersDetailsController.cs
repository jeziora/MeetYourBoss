using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetYourBoss.Models;
using System.IO;

namespace MeetYourBoss.Controllers
{ 
    public class UsersDetailsController : Controller
    {
        private MeetYourBossEntities db = new MeetYourBossEntities();

        //
        // GET: /UsersDetails/

        [Authorize]
        public ViewResult Index()
        {
            var usersdetails = db.UsersDetails.Include("aspnet_Users").Single(u => u.aspnet_Users.UserName == User.Identity.Name);
            return View(usersdetails);
        }

        //
        // GET: /UsersDetails/Details/5

        public ViewResult Details(int id)
        {
            UsersDetail usersdetail = db.UsersDetails.Single(u => u.id == id);
            return View(usersdetail);
        }

        //
        // GET: /UsersDetails/Create

        public ActionResult Create()
        {
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View();
        } 

        //
        // POST: /UsersDetails/Create

        [HttpPost]
        public ActionResult Create(UsersDetail usersdetail)
        {
            if (ModelState.IsValid)
            {
                db.UsersDetails.AddObject(usersdetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", usersdetail.users_id);
            return View(usersdetail);
        }
        
        //
        // GET: /UsersDetails/Edit/5
 
        public ActionResult Edit(int id)
        {
            UsersDetail usersdetail = db.UsersDetails.Single(u => u.id == id);
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", usersdetail.users_id);
            return View(usersdetail);
        }

        //
        // POST: /UsersDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(UsersDetail usersdetail)
        {
            if (ModelState.IsValid)
            {
                db.UsersDetails.Attach(usersdetail);
                db.ObjectStateManager.ChangeObjectState(usersdetail, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", usersdetail.users_id);
            return View(usersdetail);
        }

        //
        // GET: /UsersDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            UsersDetail usersdetail = db.UsersDetails.Single(u => u.id == id);
            return View(usersdetail);
        }

        //
        // POST: /UsersDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            UsersDetail usersdetail = db.UsersDetails.Single(u => u.id == id);
            db.UsersDetails.DeleteObject(usersdetail);
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