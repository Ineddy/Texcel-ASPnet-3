using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TexcelASPNETbyEddy.Models
{
    public class JeuViewModel
    {
        public int idJeu { get; set; }
        public string nomJeu { get; set; }
        public string descriptionJeu { get; set; }
        public string devellopeurJeu { get; set; }
        public string configurationMinimaleJeu { get; set; }
        public List<CheckBoxViewModel> lesGenres { get; set; }
        public List<CheckBoxViewModel> lesClassifications { get; set; }
        public List<CheckBoxViewModel> lesThemes { get; set; }
    }
}