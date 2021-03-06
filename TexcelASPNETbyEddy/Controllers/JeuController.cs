﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using TexcelASPNETbyEddy.Models;

namespace TexcelASPNETbyEddy.Controllers
{
    public class JeuController : Controller
    {
        // GET: Jeu

        BdTexcel_Eddy_FranckEntities bd = new BdTexcel_Eddy_FranckEntities();
        public ActionResult Index()
        {
            return View(bd.tblJeus.ToList());
        }

        [HttpPost]
        public ActionResult Index(string texte)
        {

            List<tblJeu> listeResultatRechercheJeu = new List<tblJeu>();
            var db = new BdTexcel_Eddy_FranckEntities();

            var requeteRechercheJeu = from jeu in db.tblJeus
                                             where (jeu.tag.Contains(texte)) //|| (jeu.tblTypePlateforme.nomTypePlateforme.Contains(texte)) || (plateforme.tblSE.nomSE.Contains(texte))
                                             select jeu;

            foreach (var jeu in requeteRechercheJeu)
            {

                tblJeu monJeu = new tblJeu();
                monJeu.idJeu = jeu.idJeu;
                monJeu.idProjetTest = jeu.idProjetTest;
                monJeu.nomJeu = jeu.nomJeu;
                monJeu.tag = jeu.tag;
                monJeu.tblClassifications = jeu.tblClassifications;
                monJeu.tblGenres = jeu.tblGenres;
                monJeu.tblPlateformes = jeu.tblPlateformes;
                monJeu.tblProjetTest = jeu.tblProjetTest;
                monJeu.tblThemes = jeu.tblThemes;
                monJeu.devellopeurJeu = jeu.devellopeurJeu;
                monJeu.descriptionJeu = jeu.descriptionJeu;
                monJeu.configurationMinimaleJeu = jeu.configurationMinimaleJeu; 
                listeResultatRechercheJeu.Add(monJeu);



                /*
                maPlateforme.idPlateforme = plateforme.idPlateforme;
                maPlateforme.idTypePlateforme = plateforme.idTypePlateforme;
                //maPlateforme.tblTypePlateforme.nomTypePlateforme;
                maPlateforme.configurationPlateforme = plateforme.configurationPlateforme;
                maPlateforme.codeSE = plateforme.codeSE;
                maPlateforme.nomPlateforme = plateforme.nomPlateforme;
                maPlateforme.tagPlateforme = plateforme.tagPlateforme;
                //maPlateforme.tblJeus = plateforme.tblJeus;
                maPlateforme.tblSE = plateforme.tblSE;
                maPlateforme.tblTypePlateforme = plateforme.tblTypePlateforme;
                listeResultatRecherchePlateforme.Add(maPlateforme);
                */
            }
            return View(listeResultatRechercheJeu.ToList());
            // return View(bd.tblPlateformes.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            return View(bd.tblJeus.Find(id));
        }

        public ActionResult Edit(int id = 0)
        {
            tblJeu jeu = bd.tblJeus.Find(id);

            /*var result = from genre in bd.tblGenres
                         select new
                         {
                             genre.idGenre,
                             genre.descriptionGenre,
                             Checked = ((from jg in bd.JeuGenre where (jg.idJeu == jeu.idJeu)&(jg.idGenre == genre.idGenre) select jg).Count()>0)
                         };*/

            var checkBoxListGenre = new List<CheckBoxViewModel>();
            var checkBoxListTheme = new List<CheckBoxViewModel>();
            var checkBoxListClassification = new List<CheckBoxViewModel>();

            foreach (tblGenre genre in bd.tblGenres)
            {
                CheckBoxViewModel newCheckBoxViewModel = new CheckBoxViewModel();

                newCheckBoxViewModel.Id = genre.idGenre;
                newCheckBoxViewModel.Description = genre.descriptionGenre;
                newCheckBoxViewModel.Checked = jeuDansUneListe(genre.tblJeus, jeu);
                checkBoxListGenre.Add(newCheckBoxViewModel);
            }

            foreach (tblTheme theme in bd.tblThemes)
            {
                CheckBoxViewModel newCheckBoxViewModel = new CheckBoxViewModel();

                newCheckBoxViewModel.Id = theme.idTheme;
                newCheckBoxViewModel.Description = theme.descriptionTheme;
                newCheckBoxViewModel.Checked = jeuDansUneListe(theme.tblJeus, jeu);
                checkBoxListTheme.Add(newCheckBoxViewModel);
            }

            foreach (tblClassification classification in bd.tblClassifications)
            {
                CheckBoxViewModel newCheckBoxViewModel = new CheckBoxViewModel();

                newCheckBoxViewModel.Id = classification.idClassification;
                newCheckBoxViewModel.Description = classification.descriptionClassification;
                newCheckBoxViewModel.Checked = jeuDansUneListe(classification.tblJeus, jeu);
                checkBoxListClassification.Add(newCheckBoxViewModel);
            }

            var myJeuViewModel = new JeuViewModel();

            myJeuViewModel.configurationMinimaleJeu = jeu.configurationMinimaleJeu;

            myJeuViewModel.devellopeurJeu = jeu.devellopeurJeu;

            myJeuViewModel.descriptionJeu = jeu.descriptionJeu;

            myJeuViewModel.nomJeu = jeu.nomJeu;

            /*foreach (var genre in result)
            {
                myCheckBoxList.Add(new CheckBoxViewModel { Id = genre.idGenre, Description = genre.descriptionGenre, Checked = genre.Checked });
            }*/

            myJeuViewModel.lesGenres = checkBoxListGenre;

            myJeuViewModel.lesThemes = checkBoxListTheme;

            myJeuViewModel.lesClassifications = checkBoxListClassification;

            return View(myJeuViewModel);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(JeuViewModel j)
        {
            
                /*tblJeu jeu = bd.tblJeus.Find(j.idJeu);
                jeu.devellopeurJeu = j.devellopeurJeu;
                jeu.descriptionJeu = j.descriptionJeu;
                jeu.configurationMinimaleJeu = j.configurationMinimaleJeu;
                jeu.nomJ0eu = j.nomJeu;

                foreach (tblClassification cl in bd.tblClassifications)
                {
                    foreach (tblClassification CL in jeu.tblClassifications)
                    {
                        if(CL.idClassification == cl.idClassification)
                        {
                            bd.tblClassifications.Find(cl.idClassification).tblJeus.Remove(jeu);
                        }
                    }
                }

                foreach (tblTheme th in bd.tblThemes)
                {
                    foreach (tblTheme TH in jeu.tblThemes)
                    {
                        if (TH.idTheme == th.idTheme)
                        {
                            bd.tblThemes.Find(th.idTheme).tblJeus.Remove(jeu);
                        }
                    }
                }

                foreach (tblGenre ge in bd.tblGenres)
                {
                    foreach (tblGenre GE in jeu.tblGenres)
                    {
                        if (GE.idGenre == ge.idGenre)
                        {
                            bd.tblGenres.Find(ge.idGenre).tblJeus.Remove(jeu);
                        }
                    }
                }

                foreach (tblJeu J in bd.tblJeus)
                {
                    if (J.idJeu == j.idJeu)
                    {
                        J.tblGenres.Clear();
                        J.tblClassifications.Clear();
                        J.tblThemes.Clear();
                    }
                }

                foreach (var genre in j.lesGenres)
                {
                    if(genre.Checked)
                    {

                        tblGenre newGenre = new tblGenre();
                        newGenre.descriptionGenre = genre.Description;
                        newGenre.idGenre = genre.Id;

                        bd.tblGenres.Find(genre.Id).tblJeus.Add(jeu);
                        bd.tblJeus.Find(jeu.idJeu).tblGenres.Add(newGenre);
                    }
                }

                foreach (var cl in j.lesClassifications)
                {
                    if (cl.Checked)
                    {

                        tblClassification newCL = new tblClassification();
                        newCL.descriptionClassification = cl.Description;
                        newCL.idClassification = cl.Id;

                        bd.tblClassifications.Find(cl.Id).tblJeus.Add(jeu);
                        bd.tblJeus.Find(jeu.idJeu).tblClassifications.Add(newCL);
                    }
                }

                foreach (var th in j.lesThemes)
                {
                    if (th.Checked)
                    {
                        tblTheme newTH = new tblTheme();
                        newTH.descriptionTheme = th.Description;
                        newTH.idTheme = th.Id;

                        bd.tblThemes.Find(th.Id).tblJeus.Add(jeu);
                        bd.tblJeus.Find(jeu.idJeu).tblThemes.Add(newTH);
                    }
                }
                /*foreach (tblClassification cl in bd.tblClassifications)
                {
                    foreach (tblClassification CL in jeu.tblClassifications)
                    {
                        if (CL.idClassification == cl.idClassification)
                        {
                            bd.tblClassifications.Find(cl.idClassification).tblJeus.Remove(jeu);
                        }
                    }
                }*/

                //bd.Entry(jeu).State = System.Data.Entity.EntityState.Modified;*/
                try
                {
                    tblJeu jeu = bd.tblJeus.Find(j.idJeu);

                    jeu.devellopeurJeu = j.devellopeurJeu;
                    jeu.descriptionJeu = j.descriptionJeu;
                    jeu.configurationMinimaleJeu = j.configurationMinimaleJeu;
                    jeu.nomJeu = j.nomJeu;

                    jeu.tag = jeu.nomJeu + jeu.configurationMinimaleJeu+jeu.descriptionJeu+jeu.devellopeurJeu;

                     bd.Entry(jeu).State = System.Data.Entity.EntityState.Modified;

                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(j);
                }  
        }

        public ActionResult Create()
        {   
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblJeu jeu)
        {
            jeu.tag = jeu.nomJeu + jeu.configurationMinimaleJeu + jeu.descriptionJeu + jeu.devellopeurJeu;

/* tblGenre newGenre = new tblGenre();
            newGenre.descriptionGenre = "Course";
            newGenre.idGenre = 1;

            tblClassification newClassification = new tblClassification();
            newClassification.descriptionClassification = "Classification1";
            newClassification.idClassification = 1;

            tblTheme newTheme = new tblTheme();
            newTheme.idTheme = 1;
            newTheme.descriptionTheme = "";*/

            jeu.tblThemes.Add(bd.tblThemes.Find(1));
            jeu.tblClassifications.Add(bd.tblClassifications.Find(1));
            jeu.tblGenres.Add(bd.tblGenres.Find(1));

            //jeu.tblGenres.Add(newGenre);

            bd.tblJeus.Add(jeu);
            bd.SaveChanges();
            return RedirectToAction("Index");
        }



        private bool jeuDansUneListe(ICollection<tblJeu> listeJeu, tblJeu jeu)
        {
            bool trouve = false;

            foreach(tblJeu j in listeJeu)
            {
                if (jeu.idJeu == j.idJeu)
                {
                    trouve = true;
                }
            }

            return trouve;
        }


    }
}