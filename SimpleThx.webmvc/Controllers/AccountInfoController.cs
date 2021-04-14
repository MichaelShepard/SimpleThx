﻿using Microsoft.AspNet.Identity;
using SimpleThx.Models;
using SimpleThx.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleThx.webmvc.Controllers
{

    [Authorize]
    public class AccountInfoController : Controller
    {
        // GET: AccountInfo List Page
        public ActionResult Index()
        {

            var service = CreateAccountInfoService();
            var model = service.GetAccountInfo();

            return View(model);
        }


        // GET: Create Account Info Page
        public ActionResult CreateAccountInfo()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateAccountInfo(AccountInfoCreate model)
        {

            if (!ModelState.IsValid) // This needs to be if not 
            {
                return View(model);
            }

            var service = CreateAccountInfoService();


            if (service.CreateAccountInfo(model))
            {
                TempData["SaveResult"] = "Your Account info was created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Account Info could not be created.");
            return View(model);

        }

        public ActionResult EditAccountInfo (int id)
        {
            var service = CreateAccountInfoService();
            var detail = service.GetAccountInfoByAccountID(id);
            var model = new AccountInfoEdit
            {
                AccountID = detail.AccountID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                State = detail.State,
                Country = detail.Country
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountInfo(int id, AccountInfoEdit model)
        {
             if (!ModelState.IsValid) return View(model);

             if(model.AccountID != id) // not sure this is needed
              {
                 ModelState.AddModelError("", "Id Mismatch");
                 return View(model);
              }

            var service = CreateAccountInfoService();

            if (service.UpdateAccountInfo(model))
            {
                TempData["Save Result"] = "Your account information was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your accounnt information could not be updated.");
            return View(model);


        }


        // Helper Methods
        private AccountInfoService CreateAccountInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AccountInfoService(userId);
            return service;
        }



    } // END Account Info Controller
}  // END Namespace