//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TexcelASPNETbyEddy
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTest
    {
        public int idTest { get; set; }
        public string descriptionTest { get; set; }
        public int idCategorie { get; set; }
        public int idEquipe { get; set; }
        public int idProjetTest { get; set; }
    
        public virtual tblCategorie tblCategorie { get; set; }
        public virtual tblEquipe tblEquipe { get; set; }
        public virtual tblProjetTest tblProjetTest { get; set; }
    }
}
