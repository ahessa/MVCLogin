using MVCLogin.Models;
using MVCLogin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
                return View("Index",null);
            //To authorize the username and the password we will use the database entities class to query

          
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var hashPass = PasswordEncryption.TextToEncrypt(userModel.Password);
                User userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == hashPass).SingleOrDefault();
                
                //inside this if statement we handeled the situation when there is wrong username or password
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password";
                    return View("Index", userModel);
                }
                else
                {
                    //after the login session is successfull we saved the userID, we can also save the username
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        public ActionResult LogOut()
        {
            Guid userId = (Guid)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}