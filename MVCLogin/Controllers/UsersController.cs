using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCLogin.Models;
using System.Data.Entity.Validation;
using MVCLogin.ViewModels;

namespace MVCLogin.Controllers
{
    public class UsersController : Controller
    {
        private LoginDataBaseEntities db = new LoginDataBaseEntities();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

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
        public async Task<ActionResult> Create([Bind(Include = "UserID,UserName,Password,Age,Hobby,Country,Date")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = Guid.NewGuid();
                user.Password = PasswordEncryption.TextToEncrypt(user.Password);
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var EdituserViewModel = new EditUserViewModel
            {
                Age = user.Age,
                Date = user.Date,
                Country = user.Country,
                Hobby = user.Hobby,
                UserID = user.UserID,
                UserName = user.UserName,
                Password = PasswordEncryption.TextToEncrypt(user.Password)
            };
            
            return View(EdituserViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel UserViewModel)
        {
                var user = new User
                {
                    Age = UserViewModel.Age,
                    Date = UserViewModel.Date,
                    Country = UserViewModel.Country,
                    Hobby = UserViewModel.Hobby,
                    UserID = UserViewModel.UserID,
                    UserName = UserViewModel.UserName,
                    Password = PasswordEncryption.TextToEncrypt(UserViewModel.Password)
                };
            if (ModelState.IsValid)
            {
            

                db.Entry(user).State = EntityState.Modified;
                try
                {

                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }
                return RedirectToAction("Index");
           
            }
            return View(UserViewModel);


        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
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
