using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ForOffer.Models.DB
{
    public partial class MfGeneralObservationsOffer
    {
        [Key]
        [Display(Name = "Id")]
        public int GeneralObservationOfferId { get; set; }

        [Display(Name = "Observación")]
        [Required(ErrorMessage = "Es obligatorio escribir la observación")]
        public string GeneralObservationText { get; set; }

        [Display(Name = "Lugar de servicio")]
        [Required(ErrorMessage = "Seleccione un lugar del servicio")]
        public int GeneralObservationServicesId { get; set; }

        [Display(Name = "Estado")]
        [Required (ErrorMessage="Escriba un estado")]
        public int RegState { get; set; }

        public virtual MfServicesPlace GeneralObservationServices { get; set; }
    }
}
