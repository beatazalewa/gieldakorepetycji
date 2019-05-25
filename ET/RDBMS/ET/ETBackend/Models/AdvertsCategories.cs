using System;
using System.Collections.Generic;

namespace ETBackend.Models
{
    public partial class AdvertsCategories
    {
        public int AdvertId { get; set; }
        public int CategoryId { get; set; }

        public Adverts Advert { get; set; }
        public Categories Category { get; set; }
    }
}
