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
    public class PostsController : Controller
    {
        private MeetYourBossEntities db = new MeetYourBossEntities();

        //
        // GET: /Posts/

        public ViewResult IndexMain()
        {
            var posts = db.PostsViews.OrderByDescending(i=>i.date_creation).Take(3);

            return View(posts.ToList());
        }

        public ViewResult Index()
        {
            var posts = db.PostsViews;

            return View(posts.ToList());
        }

        public ActionResult FindCompany(int id )
        {
            return RedirectToAction("Details","Companies",new {id=id});
        }

        //
        // GET: /Posts/Details/5

        public ViewResult Details(int id)
        {
            Post posts = db.Posts.Single(p => p.id == id);
            return View(posts);
        }

        //
        // GET: /Posts/Create

        public ActionResult Create()
        {
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            ViewBag.companies_id = new SelectList(db.Companies, "id", "name");
            return View();
        } 

        //
        // POST: /Posts/Create

        [HttpPost]
        public ActionResult Create(Post posts)
        {
            if (ModelState.IsValid)
            {
                db.Posts.AddObject(posts);
                posts.date_creation = DateTime.Today;
                posts.date_last_view = DateTime.Today;
                //posts.users_id = "6d3952a6-27db-4996-85cf-6828988dbc30";
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", posts.users_id);
            ViewBag.companies_id = new SelectList(db.Companies, "id", "name", posts.companies_id);
            return View(posts);
        }
        
        //
        // GET: /Posts/Edit/5
 
        public ActionResult Edit(int id)
        {
            Post posts = db.Posts.Single(p => p.id == id);
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", posts.users_id);
            ViewBag.companies_id = new SelectList(db.Companies, "id", "name", posts.companies_id);
            return View(posts);
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        public ActionResult Edit(Post posts)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(posts);
                db.ObjectStateManager.ChangeObjectState(posts, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_id = new SelectList(db.aspnet_Users, "UserId", "UserName", posts.users_id);
            ViewBag.companies_id = new SelectList(db.Companies, "id", "name", posts.companies_id);
            return View(posts);
        }

        //
        // GET: /Posts/Delete/5
 
        public ActionResult Delete(int id)
        {
            Post posts = db.Posts.Single(p => p.id == id);
            return View(posts);
        }

        //
        // POST: /Posts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Post posts = db.Posts.Single(p => p.id == id);
            db.Posts.DeleteObject(posts);
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