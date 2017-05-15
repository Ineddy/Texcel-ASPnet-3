using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TexcelASPNETbyEddy.Models
{
    public class JeuGenre
    {
        public int idJeuGenre { get; set; }

        public int idJeu { get; set; }

        public int idGenre { get; set; }

        public virtual tblJeu jeu { get; set; }

        public virtual tblGenre genre { get; set; }

    }
}