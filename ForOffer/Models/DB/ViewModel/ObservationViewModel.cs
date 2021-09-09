using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForOffer.Models.DB.ViewModel
{
    public class ObservationViewModel
    {


        public int GeneralObservationOfferId { get; set; }

        [Display(Name = "Observación")]
        [Required(ErrorMessage = "Es obligatorio escribir la observación")]
        public string GeneralObservationText { get; set; }

        [Display(Name = "Lugar de servicio")]
        [Required(ErrorMessage = "Seleccione un lugar del servicio")]
        public int GeneralObservationServicesId { get; set; }

      public string ServicesPlace { get; set; }
        public int RegState { get; set; }

       public  List<ObservationViewModel> Lista { get; set; }

    }
}
