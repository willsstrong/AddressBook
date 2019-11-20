﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AddressBook.DAL;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class PeopleController : Controller
    {
        private AddressContext db = new AddressContext();

        // GET: People
        public ViewResult Index(string sortOrder, string searchString)
        {

            //allow for interactive sorting by name(first or last), address, etc
            ViewBag.LastNameSortParam = string.IsNullOrEmpty(sortOrder) ? "l-name_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "First Name" ? "f-name_desc": "First Name";
            //Add more sort options; City, Province.
            var people = from s in db.Person
                         select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                people = people.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "l-name_desc":
                    people = people.OrderByDescending(s => s.LastName);
                    break;
                case "f-name_desc":
                    people = people.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    people = people.OrderByDescending(s => s.LastName);     //list will sort by last name as default
                    break;
            }

            return View(people.ToList());
        }

        //Record Search Page  
        public ViewResult Search(string searchString)
        {
      
            var people = from s in db.Person select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Right now I am only searching by first name or last name.  It would be more functional to
                // be able to search by any field. 
                people = people.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }
            //Right now the seach page displays all records on start.  I want it to only show the heading and 
            //no records until a search is requested.
            return View(people.ToList());
        }


        public ActionResult AddrsTabSection(string sortOrder)  // alphabatized index tab contents
        {

            //click on letter tab
            //pass letter label to controller
            //query database for all people with last name start with 'letter'
            //pass resulting list to AddrsTabSection partial view
            //view builds the tab content and passes to People/Index

            //allow for interactive sorting by name(first or last), address, etc
            ViewBag.LastNameSortParam = string.IsNullOrEmpty(sortOrder) ? "l-name_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "First Name" ? "f-name_desc" : "First Name";
            var people = from s in db.Person
                         select s;
            switch (sortOrder)
            {
                case "l-name_desc":
                    people = people.OrderByDescending(s => s.LastName);
                    break;
                case "f-name_desc":
                    people = people.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    people = people.OrderByDescending(s => s.LastName);     //list will sort by last name by default
                    break;
            }

            return PartialView(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,Address,City,Province,PostCode,PhoneNumMain,PhoneNumCell,Email")] Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Person.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,Address,City,Province,PostCode,PhoneNumMain,PhoneNumCell,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
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
