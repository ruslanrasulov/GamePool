﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GamePool.PL.MVC.Models.Product
{
    public class DisplayGameVm
    {
        public int Id { get; set; }

        public int? AvatarId { get; set; }

        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Genres:")]
        public string Genres { get; set; }

        [Display(Name = "Price:")]
        public double Price { get; set; }

        [Display(Name = "ReleaseDate:")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public decimal? Rating { get; set; }

        public DisplaySystemRequirementsVm MinimalSystemRequirements { get; set; }

        public DisplaySystemRequirementsVm RecommendedSystemRequirements { get; set; }
    }
}