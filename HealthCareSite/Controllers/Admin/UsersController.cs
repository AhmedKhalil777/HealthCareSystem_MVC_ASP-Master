using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthCareSite.Models;

namespace HealthCareSite.Controllers
{
    public class UsersController : Controller
    {
        // Dependancy Injection
        private health_care_systemEntities1 db = new health_care_systemEntities1();

        #region Chat
        //Chatting Room For User
        [HttpGet]
        public ActionResult Chat(int? id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }
        #endregion

        #region Index
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        #endregion

        #region Profile Manager
        [HttpGet]
        public ActionResult UserProfile(int id) => View(db.Users.Single(d => d.User_ID ==id));
        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            string filename = Path.GetFileNameWithoutExtension(user.Picfile.FileName);
            string extension = Path.GetExtension(user.Picfile.FileName);
            filename += DateTime.Now.ToString("yymmssfff") + extension;
            user.Pic = "../../Uploads/" + filename;
            filename = Path.Combine(Server.MapPath("~/Uploads/"), filename);
            user.Picfile.SaveAs(filename);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");

            }

            return View();
        }

 
        #endregion

        #region Doctors_User Manager
        public ActionResult Doctors(int Id)
        {
            List<Doctor> doctors = db.Doctors.ToList();
            List<Doctor> disdoctors = new List<Doctor>();
            foreach (var item in db.User_Doctor.ToList())
            {
                if (item.User_User_ID == Id)
                {
                    disdoctors.Add(item.Doctor);
                }
            }
            foreach (var item in disdoctors)
            {
                doctors.Remove(item);
            }
           
            return View(doctors);
        }
        #endregion

        #region Game
        public ActionResult Game(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        #endregion

        #region Details Admin
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        #endregion

        #region UI_Home
        public ActionResult Home(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        #endregion

        #region Create

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,Name,Gender,Height,Weight,Blood_pressure,E_mail,Pic,Blood_sugar,Description,Status,Phone,Password,Disease")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        #endregion

        #region Edit
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,Name,Gender,Height,Weight,Blood_pressure,E_mail,Pic,Blood_sugar,Description,Status,Phone,Password,Disease")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        #endregion

    
        [HttpPost]
        public ActionResult Search(string Text, string Type)
        {
            if (Type == "N")
            {
                return View("Doctors",db.Doctors.Where(d => d.Name == Text).ToList());
            }
            return View("Doctors", db.Doctors.Where(d => d.Speciality == Text).ToList());
        }
        #region Login

        [HttpPost]
        public ActionResult Login( string name , string password)
        {
            
            if (name == null || password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Where(u => u.Name == name).FirstOrDefault();
            if (name == user.Name && password ==user.Password)
            {
                Cookie cookie = new Cookie("userid" ,user.User_ID.ToString());
                Cookie pic = new Cookie("pic", user.Pic.ToString());
                Response.Cookies["cookie"].Value = user.User_ID.ToString();
                Response.Cookies["pic"].Value = user.Pic.ToString();
                return Redirect("Home?id=" + user.User_ID);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
        }
        #endregion

        #region
        [HttpGet]
        public ActionResult Connect(int Id) {
            foreach (var item in db.User_Doctor.ToList())
            {
                if (item.Doctor_Doc_ID ==Id)
                {
                    ViewBag.Connect = true;
                }
            }  

           return View(db.Doctors.Single(d => d.Doc_ID == Id));
        }
       

        [HttpPost]
        public ActionResult Connect(string DID , string SID)
        {

            db.User_Doctor.Add(new User_Doctor { User_User_ID = int.Parse(SID), Doctor_Doc_ID = int.Parse(DID), Interact_des = "connection"  , Report= "dferwgwethteh"});
            db.SaveChanges();
            return RedirectToAction("Doctors" ,SID);
        }

        [HttpGet]
        public ActionResult Cdoc(int Id) {

           var docuser= db.Users.Single(u => u.User_ID == Id).User_Doctor.ToList();
            List<Doctor> doctors = new List<Doctor>();
            for (int i = 0; i < docuser.Count; i++)
            {

                doctors.Add(docuser[i].Doctor);
            }
            return View("CDoctors", doctors);
        }
        #endregion
        #region Delete
        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        [HttpGet]
        public ActionResult Drug() { return View(db.Drugs.ToList()); }
        [HttpGet]
        public ActionResult Food() { return View(db.Foods.ToList()); }
        [HttpGet]
        public ActionResult Excercise() { return View(db.Exercises.ToList()); }
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
