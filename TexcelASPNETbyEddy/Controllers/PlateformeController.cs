using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace TexcelASPNETbyEddy.Controllers
{
    public class PlateformeController : Controller
    {
        // GET: Plateforme
        BdTexcel_Eddy_FranckEntities bd = new BdTexcel_Eddy_FranckEntities();
        public ActionResult Index()
        {
            return View(bd.tblPlateformes.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            return View(bd.tblPlateformes.Find(id));
        }
        public ActionResult Create()
        {
            //ViewBag.ErreurCreateSE = false;
            ViewBag.TypePlateformes = new SelectList(bd.tblTypePlateformes, "idTypePlateforme","nomTypePlateforme");

            ViewBag.SE = new SelectList(bd.tblSEs, "codeSE", "nomSE");

            //ViewData["TypePlateformes"] = new SelectList(bd.tblTypePlateformes, "idTypePlateforme", "nomTypePlateforme");

            //ViewData["SE"] = new SelectList(bd.tblSEs, "codeSE", "nomSE");

            return View();
        }

        [HttpPost]
        public ActionResult Create(tblPlateforme plateforme)
        {
            try
            {

                ViewBag.TypePlateformes = new SelectList(bd.tblTypePlateformes, "idTypePlateforme", "nomTypePlateforme");

                ViewBag.SE = new SelectList(bd.tblSEs, "codeSE", "nomSE");

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (existenceDeLaPlateforme(plateforme) == true)
                    {
                        ViewBag.ErreurPlateforme = true;
                        ViewBag.Message = " Cette plateforme existe déja!!! ";

                        return View(plateforme);
                    }
                    else
                    {
                        // plateforme.tagPlateforme = plateforme.nomPlateforme + plateforme.configurationPlateforme + plateforme.tblTypePlateforme.nomTypePlateforme + plateforme.tblSE.nomSE;
                        plateforme.tagPlateforme = plateforme.nomPlateforme + plateforme.configurationPlateforme;
                        bd.tblPlateformes.Add(plateforme);
                        bd.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }

                return View(plateforme);
            }
            catch
            {
                return View(plateforme);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            //ViewData["TypePlateformes"] = new SelectList(bd.tblTypePlateformes, "idTypePlateforme", "nomTypePlateforme");

            //ViewData["SE"] = new SelectList(bd.tblSEs, "codeSE", "nomSE");

            ViewBag.TypePlateformes = new SelectList(bd.tblTypePlateformes, "idTypePlateforme", "nomTypePlateforme");

            ViewBag.SE = new SelectList(bd.tblSEs, "codeSE", "nomSE");

            return View(bd.tblPlateformes.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(tblPlateforme Plateforme)
        {

            try
            {
                Plateforme.tagPlateforme = Plateforme.nomPlateforme + Plateforme.configurationPlateforme;

                bd.Entry(Plateforme).State = EntityState.Modified;

                bd.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(Plateforme);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(bd.tblPlateformes.Find(id));
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            tblPlateforme plateforme = bd.tblPlateformes.Find(id);

            try
            {
                // TODO: Add delete logic here
                if (nombreJeuPlateforme(plateforme) != 0)
                {
                    ViewBag.ErreurPlateforme = true;
                    ViewBag.Message = " Impossible!!! La plateforme est liée à un ou plusieurs jeux. ";

                    return View(plateforme);
                }
                else
                {
                    bd.tblPlateformes.Remove(plateforme);

                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View(plateforme);
            }

        }

        private bool existenceDeLaPlateforme(tblPlateforme plateforme)
        {
            List<tblPlateforme> listeDesPlateforme = bd.tblPlateformes.ToList();
            bool existeDansLaBD = false;

            foreach (tblPlateforme pl in listeDesPlateforme)
            {
                if (pl.nomPlateforme == plateforme.nomPlateforme && pl.configurationPlateforme == plateforme.configurationPlateforme && pl.idTypePlateforme == plateforme.idTypePlateforme&& pl.codeSE == plateforme.codeSE)
                {
                    existeDansLaBD = true;
                    break;
                }
            }

            return existeDansLaBD;
        }

        private int nombreJeuPlateforme(tblPlateforme Plateforme)
        {
            int nbreJeuDeLaPlateforme = 0;
            var requeteNbreJeuDeLaPlateforme = from plateforme in bd.tblPlateformes
                                               where plateforme.idPlateforme == Plateforme.idPlateforme
                                               select plateforme.tblJeus.Count();

            foreach (var nombre in requeteNbreJeuDeLaPlateforme)
            {
                nbreJeuDeLaPlateforme = nombre;
            }

            return nbreJeuDeLaPlateforme;

        }

        private List<tblPlateforme> listeDesPlateformes()
        {

            List<tblPlateforme> listeDesPlateformes = new List<tblPlateforme>();

            var query = from plateforme in bd.tblPlateformes
                        orderby plateforme.idPlateforme
                        select plateforme;

            foreach (var plateforme in query)
            {
                tblPlateforme newPlateforme = new tblPlateforme();
                newPlateforme.idPlateforme = plateforme.idPlateforme;
                newPlateforme.nomPlateforme = plateforme.nomPlateforme;
                newPlateforme.configurationPlateforme = plateforme.configurationPlateforme;
                newPlateforme.idTypePlateforme = plateforme.idTypePlateforme;
                newPlateforme.codeSE = plateforme.codeSE;
               
                listeDesPlateformes.Add(newPlateforme);
            }

            return listeDesPlateformes;
        }

    }
}