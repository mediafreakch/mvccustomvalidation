using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomValidationExample.Models;

namespace CustomValidationExample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/
        [HttpPost]
        public ActionResult Index(ArticleModel model)
        {
            return View(model);
        }
    }
}
