using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AddressBook.DAL;
using AddressBook.Models;
using PagedList;

namespace AddressBook.Controllers
{
    public class PeopleController : Controller
    {
        private AddressContext db = new AddressContext();

        // GET: People
        public ViewResult Index(string TabID)
        {
            //page will load Tab 'A' by default, or whatever Tab was selected
            ViewBag.TabID = string.IsNullOrEmpty(TabID) ? "A" : TabID;
            TabID = string.IsNullOrEmpty(TabID) ? "A" : TabID;  //without this check Tab 'A' would be active but empty when
                                                                //returning from another view

            //GET 
            var people = from s in db.Person select s;

            //asks for records where last name start with the letter in TabID
            var query = people.Where(s => s.LastName.StartsWith(TabID));

            //  Passes list of records to People/Index
            return View(query.ToList());
        }

        // Partial View for displaying Address Data in a table
        public PartialViewResult AddressTable()
        {   
            return PartialView();
        }

        //Record Search Page  
        public ViewResult Search(string searchString)
        {
            var people = from s in db.Person select s;

            var query = people.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));

            return View(query.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id, string TabID)
        {
            ViewBag.TabID = TabID;
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
        public ActionResult Create(string TabID)
        {
            ViewBag.TabID = TabID;
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,Address,City,Province,PostCode,PhoneNumMain,PhoneNumCell,Email")] Person person, string TabId)
        {
            ViewBag.TabID = string.IsNullOrEmpty(TabId) ? "A":TabId;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Person.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("Index",new {TabID = TabId });
                }
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id, string TabID)
        {
            ViewBag.TabID = TabID;
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
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,Address,City,Province,PostCode,PhoneNumMain,PhoneNumCell,Email")] Person person, string tabID)
        {
            ViewBag.TabID = tabID;
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {TabID = tabID });
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id, string TabID)
        {
            ViewBag.TabID = TabID;
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
        public ActionResult DeleteConfirmed(int id, string tabID)
        {
            ViewBag.TabID = tabID;
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index",new {TabID = tabID });
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

