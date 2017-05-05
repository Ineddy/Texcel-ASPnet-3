using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace TexcelASPNETbyEddy.Controllers
{
    public class SEController : Controller
    {
        // GET: SE
        BdTexcel_Eddy_FranckEntities bd = new BdTexcel_Eddy_FranckEntities();
        public ActionResult Index()
        {
            return View(bd.tblSEs.ToList());
        }

        // GET: tblSE/Details/5
        public ActionResult Details(int id = 0)
        {
            return View(bd.tblSEs.Find(id));
        }

        // GET: tblSE/Create
        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.ErreurCreateSE = false;
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
                    if(existenceDuSE(SE) == true)
                    {
                        ViewBag.ErreurSE = true;
                        ViewBag.Message = " Ce système d'exploitation existe déja!!! ";

                        return View(SE);
                    }
                    else
                    {
                        SE.tagSE = SE.nomSE + SE.editionSE + SE.versionSE;
                        bd.tblSEs.Add(SE);
                        bd.SaveChanges();
                        return RedirectToAction("Index");
                    }
                   
                }

                return View(SE);
            }
            catch
            {
                return View(SE);
            }
        }

        // GET: tblSE/Edit/5
        public ActionResult Edit(int id=0)
        {
            return View(bd.tblSEs.Find(id));
        }

        // POST: tblSE/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(tblSE SE)
        {
            
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    if (existenceDuSE(SE) == true)
                    {
                        ViewBag.ErreurSE = true;
                        ViewBag.Message = " Ce système d'exploitation existe déja!!! ";

                        return View(SE);
                    }
                    else
                    {
                        //SE.tagSE = SE.nomSE + SE.editionSE + SE.versionSE;
                        
                        bd.Entry(SE).State = EntityState.Modified;

                        bd.SaveChanges();

                        return RedirectToAction("Index");
                    }

                }

                return View(SE);
            /*}
            catch
            {
                return View(SE);
            }*/
        }

        // GET: tblSE/Delete/5
        public ActionResult Delete(int id)
        {
            return View(bd.tblSEs.Find(id));
        }

        // POST: tblSE/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            tblSE SE = bd.tblSEs.Find(id);

            try
            {
                // TODO: Add delete logic here
                

                if (nombrePlateformeSE(SE) != 0)
                {
                    ViewBag.ErreurSE = true;
                    ViewBag.Message = " Le système d'exploitation est lié a une ou plusieurs plateformes!!! ";

                    return View(SE);
                }
                else
                {
                    bd.tblSEs.Remove(SE);

                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View(SE);
            }

        }

        private bool existenceDuSE(tblSE SE)
        {
            List<tblSE> listeDesSE = this.listeDesSE();
            bool existeDansLaBD = false;

            foreach (tblSE se in listeDesSE)
            {
                if (se.nomSE == SE.nomSE && se.editionSE == SE.editionSE && se.versionSE == SE.versionSE)
                {
                    existeDansLaBD = true;
                    break;
                }
            }

            return existeDansLaBD;
        }

        private int nombrePlateformeSE(tblSE SE)
        {
            int nbrePlateformeSE = 0;
            var requeteNbrePlateformeSE = from se in bd.tblSEs
                                          where se.codeSE == SE.codeSE
                                          select se.tblPlateformes.Count();

            foreach (var nombre in requeteNbrePlateformeSE)
            {
                nbrePlateformeSE = nombre;
            }

            return nbrePlateformeSE;
        } 

        private List<tblSE> listeDesSE()
        {
           
            List<tblSE> listeDesSE = new List<tblSE>();

            var query = from se in bd.tblSEs
                        orderby se.codeSE
                        select se;

            foreach (var se in query)
            {
                tblSE SE = new tblSE();
                SE.codeSE = se.codeSE;
                SE.nomSE = se.nomSE;
                SE.editionSE = se.editionSE;
                SE.versionSE = se.versionSE;

                listeDesSE.Add(SE);
            }

            return listeDesSE;
        }

    }
}
