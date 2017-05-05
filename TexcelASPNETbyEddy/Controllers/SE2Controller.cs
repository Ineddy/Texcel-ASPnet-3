using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace TexcelASPNETbyEddy.Controllers
{
    public class SE2Controller : Controller
    {
        // GET: tblSE
        BdTexcel_Eddy_FranckEntities bd = new BdTexcel_Eddy_FranckEntities();
        public ActionResult Index()
        {
            return View(bd.tblSEs.ToList());
        }

        // GET: tblSE/Details/5
        public ActionResult Details(int id=0)
        {
            return View(bd.tblSEs.Find(id));
        }

        // GET: tblSE/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblSE/Create
        [HttpPost]
        public ActionResult Create(tblSE SE)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    bd.tblSEs.Add(SE);
                    bd.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tblSE/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tblSE/Edit/5
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(tblSE SE)
        {
            try
            {
                // TODO: Add update logic here

                bd.Entry(SE).State = EntityState.Modified;

                bd.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tblSE/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tblSE/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here

                tblSE SE = bd.tblSEs.Find(id);
                bd.tblSEs.Remove(SE);
                bd.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
    }
}
