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
    
    public partial class tblPlateforme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPlateforme()
        {
            this.tblJeus = new HashSet<tblJeu>();
        }
    
        public int idPlateforme { get; set; }
        public string nomPlateforme { get; set; }
        public string configurationPlateforme { get; set; }
        public int idTypePlateforme { get; set; }
        public int codeSE { get; set; }
        public string tagPlateforme { get; set; }
    
        public virtual tblSE tblSE { get; set; }
        public virtual tblTypePlateforme tblTypePlateforme { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblJeu> tblJeus { get; set; }
    }
}
