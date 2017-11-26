using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamePool.PL.MVC.Models.Search
{
    public class SearchParametersVM
    {
        [Display(Name = "Name:")]
        public string GameName { get; set; }

        [Display(Name = "Price from:")]
        public int? PriceFrom { get; set; }

        [Display(Name = "Price to:")]
        public int? PriceTo { get; set; }

        [Display(Name = "Release date:")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Genres:")]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        public int Page { get; set; }
    }
}