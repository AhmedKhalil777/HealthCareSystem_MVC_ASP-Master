using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthCareSite.Models;

namespace HealthCareSite.Controllers
{
    public class DoctorsController : Controller
    {
        private health_care_systemEntities1 db = new health_care_systemEntities1();

        // GET: Doctors
        [HttpGet]
        public ActionResult Index()
        {

            return View(db.Doctors.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }



        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Doc_ID,Name,E_mail,Password,Gender,Evaluation,Phone,Details,Speciality,Pic")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Doc_ID,Name,E_mail,Password,Gender,Evaluation,Phone,Details,Speciality,Pic")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DoctorProfile(int Id) => View(db.Doctors.Single(d => d.Doc_ID == Id));

        [HttpPost]
        public ActionResult Login(string Name, string Password)
        {
            if (Name == null || Password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doc = db.Doctors.SingleOrDefault(n => n.Name == Name);
            if (doc.Password == Password)
            {
                Cookie Doctor = new Cookie("DID", doc.Doc_ID.ToString());
                Response.Cookies["DID"].Value = doc.Doc_ID.ToString();
                Cookie DIMG = new Cookie("DIMG", doc.Doc_ID.ToString());
                Response.Cookies["DIMG"].Value = doc.Pic.ToString();
                return RedirectToAction("DoctorProfile", new { Id = doc.Doc_ID });

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        }
        [HttpGet]
        public ActionResult Users(int Id){
           var ud=  db.User_Doctor.Where(d => d.Doctor_Doc_ID == Id);
            var users = new List<User>();
            foreach (var item in ud)
            {
               users.Add( db.Users.Single(u => u.User_ID == item.User_User_ID));
            }
            return View(users);
                
                }
        [HttpGet]
        public ActionResult chat(int id) => View(db.Doctors.Single(d=> d.Doc_ID ==id));

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
