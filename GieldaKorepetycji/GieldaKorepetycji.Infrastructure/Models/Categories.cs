using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GieldaKorepetycji.Infrastructure.Models
{
    public partial class Categories
    {
        public Categories()
        {
            AdvertsCategories = new HashSet<AdvertsCategories>();
        }

        public int CategoryId { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }

        public ICollection<AdvertsCategories> AdvertsCategories { get; set; }
    }
}
