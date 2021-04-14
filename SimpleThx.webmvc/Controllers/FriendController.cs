using SimpleThx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var model = new FriendList[0];
            return View(model);
        }


        // GET: Create Friend View

        public ActionResult CreateFriend()
        {
            return View();
        }

        // POST: Create Friend 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFriend(FriendCreate model)
        {
            if(ModelState.IsValid)
            {

            }

            return View(model);
        }





    } // END Friend Controller
} // END Namespace