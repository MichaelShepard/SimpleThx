using Microsoft.AspNet.Identity;
using SimpleThx.Data;
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


        public ActionResult getFriendByID(int id)
        {

            var service = CreateFriendService();
            var model = service.GetFriendByID(id);


            return View(model);
        }


        public ActionResult SearchForFriends(string searchString)
        {

            var service = CreateFriendService();
            var model = service.FriendSearch(searchString);

            return View(model);
        }

        public ActionResult ConnectFriends(int id)
        {
            var service = CreateFriendService();
            var model = service.CreatesFriendListModel(id);
            

            if (service.PostConnection(model))
            {
                TempData["SaveResult"] = "Your connection was sent.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry something went wrong. Please try again.");

            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ConnectFriendsPost(FriendList model)
        //{


        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);

        //    }

        //    var service = CreateFriendService();


        //    if (service.PostConnection(model))
        //    {
        //        TempData["SaveResult"] = "Your connection was created.";
        //        return new RedirectResult("Index");
        //    };

        //    ModelState.AddModelError("", "You were not connected.");

        //    return View(model);




        //}

        public ActionResult UpdateFriend(int id, int status)
        {
            var service = CreateFriendService();
            var model = service.GetFriendListModel(id);

            FriendStatus newStatus = FriendStatus.Pending;

            if(status == 1) {

                newStatus = FriendStatus.Accepted;

            } else if (status == 2) {

                newStatus = FriendStatus.Declined;

            }

            
            if (service.PostUpdateFriend(model, newStatus))
            {
                TempData["SaveResult"] = "You are connected";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry something went wrong. Please try again.");

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