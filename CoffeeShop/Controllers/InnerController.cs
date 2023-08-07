using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class InnerController : Controller
    {
        // GET: Inner
        public ActionResult Index()
        {
            return View();
        }
    }
}