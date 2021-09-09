using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ForOffer.Models.DB
{
    public partial class MfServicesPlace
    {
        public MfServicesPlace()
        {
            //MfGeneralObservationsOffers = new HashSet<MfGeneralObservationsOffer>();
        }
        [Key]
        public int ServicesPlaceId { get; set; }
        public string ServicesPlaces { get; set; }

        //public virtual ICollection<MfGeneralNotesOffer>  GeneralNotesServicesId { get; set; }
        //public virtual ICollection<MfGeneralObservationsOffer> MfGeneralObservationsOffers { get; set; }
    }
}
