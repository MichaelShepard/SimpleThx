using Microsoft.AspNet.Identity;
using SimpleThx.Models;
using SimpleThx.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleThx.webmvc.Controllers
{

    [Authorize]
    public class FriendController : Controller
    {
        // GET: Friend
        public ActionResult Index()
        {

            var service = CreateFriendService();
            var model = service.GetFriends();
            
           
            return View(model);
        }

        public ActionResult SearchForFriends(string searchString)
        {

            var service = CreateFriendService();
            var model = service.FriendSearch(searchString);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectFriends(FriendCreate model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateFriendService();

            if (service.AddConnectionWithFriend(model))

            {
                TempData["SaveResult"] = "Your connection has been made";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Connection could not be made.");

            return View(model);


        }





            // Helper

            private FriendService CreateFriendService()
        {

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FriendService(userId);
            return service;

        }


    } // END Friend Controller
} // END Namespace