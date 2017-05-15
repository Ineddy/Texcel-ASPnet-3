using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TexcelASPNETbyEddy.Controllers
{
    public class GerantController : Controller
    {
        // GET: Gerant
        BdTexcel_Eddy_FranckEntities bd = new BdTexcel_Eddy_FranckEntities();
        public ActionResult Index()
        {
            Session["NomPrenom"] = null;
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["UserPassword"] = null;
            Session["Role"] = null;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(tblGerant gerant)
        {
            try
            {
                var usr = bd.tblGerants.Single(u => u.loginGerant == gerant.loginGerant && u.motDePasseGerant == gerant.motDePasseGerant);

                var query = from employe in bd.tblEmployes
                            where employe.idEmploye == usr.idGerant
                            select employe;

                foreach(var employe in query)
                {
                    Session["NomPrenom"] = employe.prenomEmploye +" "+ employe.nomEmploye;
                }

                Session["UserID"] = usr.idGerant.ToString();
                Session["UserName"] = usr.loginGerant.ToString();
                Session["UserPassword"] = usr.motDePasseGerant.ToString();
                Session["Role"] = usr.roleGerant.ToString();

                return RedirectToAction("Index","Home");
            }
            catch
            {
                ModelState.AddModelError(" ", " User or password are wrong!!!! ");

                ViewBag.ErrorLogin = true;

                return View();
            }
        }

    }
}