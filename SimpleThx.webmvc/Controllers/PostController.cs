using Microsoft.AspNet.Identity;
using SimpleThx.Models;
using SimpleThx.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleThx.webmvc.Controllers
{
    public class PostController : Controller
    {
        // GET: Post List Page
        public ActionResult Index()
        {
            var service = CreatePostService();
            var model = service.GetPosts();

            return View(model);
        }


        // GET: Create Account Info Page
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreatePost(PostCreate model)
        {

            if (ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreatePostService();


            if (service.CreatePost(model))
            {
                ViewBag.SaveResult = "Your Account info was created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Account Info could not be created.");
            return View(model);

        }



        // Helper Methods
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            return service;
        }


    } // END Post Controller
} // END Namespace