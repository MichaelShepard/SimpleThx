using Microsoft.AspNet.Identity;
using SimpleThx.Data;
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

        public ActionResult CreatePost(PostCreate model, Guid id)
        {
            var service = CreatePostService();
            var entity = service.PostModelCreate(model, id);


            //var service = CreatePostService();

            if (service.CreatePost(entity))
            {
                ViewBag.SaveResult = "You Posted!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Post could not be created.");
            return View(model);

        }


        public ActionResult UpdatePost(int id, int status)
        {
            var service = CreatePostService();
            var model = service.GetPostListModel(id);

            Status newStatus = Status.Pending;

            if (status == 1)
            {
                newStatus = Status.Accepted;

            }
            else if (status == 2)
            {

                newStatus = Status.Declined;

            }

            if (service.UpdatePostStatus(model, newStatus))
            {
                TempData["SaveResult"] = "You accepted post";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry something went wrong. Please try again.");

            return View(model);

        } // END Update Post

        public ActionResult EditPost(int id)
        {

            var service = CreatePostService();
            var detail = service.GetPostByID(id);
            var model = new PostEdit
            {

                PostID = detail.PostID,
                PostUserID = detail.PostUserID,
                Title = detail.Title,
                Content = detail.Content,
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditPost(int id, PostEdit model)
        {

            if (!ModelState.IsValid) return View(model);

            if (model.PostID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePostService();

            if (service.UpdatePostContent(model))
            {
                TempData["SaveResult1"] = "Your Post was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Post could not be updated.");
            return View(model);
        } // END Edit Post
    
    


        // Helper Methods
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            return service;
        }



        

    } // END Post Controller
} // END Namespace