using System;
using System.Collections.Generic;

namespace GamePool.PL.MVC.Models.Admin
{
    public class CreateGameVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<int> GenreIds { get; set; }
    }
}