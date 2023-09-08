using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DnD_CMS.DAL;
using DnD_CMS.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DnD_CMS.Controllers
{
    public class UserController : Controller
    {
        User_DAL User_DAL = new User_DAL();
        IWebHostEnvironment webHostEnvironment;
        public UserController(IWebHostEnvironment _environment)
        {
            webHostEnvironment = _environment;
        }

        // GET: UserController

        /*
            Retrieves all users, if user is not logged in it sends user to Register page.
        */
        public ActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUser)))
            {
                return RedirectToAction("Register", "User");
            };

            var userList = User_DAL.getAllUsers();
            return View(userList);
        }


        // GET: UserController/Create
        /*
            Sends user to Register View upon a GET request.
        */
        public ActionResult Register()
        {
            return View();
        }


        // POST: UserController/Create
        /*
            Gets user data from user Create View and sends info to userDAL. Sends user to site homescreen upon successful 
            account creation and marks user as logged into newly created account using a session variable
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(userModel user)
        {
            bool isInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = User_DAL.insertUsers(user);

                    if (isInserted)
                    {
                        HttpContext.Session.SetString(SessionVariables.SessionKeyUser, user.userName);
                        TempData["SuccessMessage"] = "User Account Created Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Whoops... Unable to Create User Account";
                    }
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // GET: UserController/Login
        /*
            Sends user to Login View upon a GET request.
        */
        public ActionResult Login()
        {
            return View();
        }


        // POST: UserController/Login
        /*
            Gets user data from user Login View and sends info to userDAL. Sends user to site homescreen upon successful 
            verification of credentials for the account and marks user as logged into the account using a session variable
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userModel user)
        {
            bool isValidated = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isValidated = User_DAL.validateUser(user);

                    if (isValidated)
                    {
                        HttpContext.Session.SetString(SessionVariables.SessionKeyUser, user.userName);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Whoops... Unable to validate User Account";
                        return RedirectToAction("Login");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
