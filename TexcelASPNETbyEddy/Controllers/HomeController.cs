﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TexcelASPNETbyEddy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactez-nous";
            ViewBag.Eddy = "Eddy Fontaine";
            ViewBag.Franck = "Franck Michael Atongfor";


            return View();
        }
    }
}