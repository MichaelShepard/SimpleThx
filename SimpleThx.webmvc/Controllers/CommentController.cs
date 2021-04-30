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
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            var service = CreateCommentService();
            var model = service.GetComments();

            return View(model);
        }


        public ActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateComment(CommentCreate model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var service = CreateCommentService();
            //var entity = service.CommentModelCreate(model, id);

            if(service.CreateComment(model))
            {
                ViewBag.SaveResult = "You Commented!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Comment could not be created.");
            return View(model);
        }




        // Helper Methods

        private CommentService CreateCommentService()
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CommentService(userId);
            return service;

        }


    } // END Comment Controller
} // END Namespace