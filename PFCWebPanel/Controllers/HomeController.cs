using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PFCWebPanel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {

            //asteriskcdrdbEntities astEntities=new asteriskcdrdbEntities();

            //ViewBag.Hello = astEntities.cdr.OrderByDescending(x=>x.uniqueid).Select(x => x.uniqueid).FirstOrDefault();
            return View();
        }
    }
}