using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ForOffer.Models.DB
{
    public partial class MfGeneralNotesOffer
    {
        [Key]
        [Display(Name ="Id")]
        public int GeneralNotesOfferId { get; set; }
        [Display(Name ="Nota")]
        [Required(ErrorMessage ="Es obligatorio hacer una anotación")]
        public string GeneralNotesText { get; set; }

        [Display(Name = "Lugar de servicio")]
        [Required(ErrorMessage ="Seleccione un lugar del servicio")]
        public int GeneralNotesServicesId { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public int RegState { get; set; }

        public virtual MfServicesPlace GeneralNotesServices { get; set; }
    }
}
