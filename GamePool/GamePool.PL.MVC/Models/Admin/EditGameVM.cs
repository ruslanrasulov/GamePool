using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GamePool.PL.MVC.Models.Admin
{
    public class EditGameVm : IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        public int? AvatarId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Price:")]
        public decimal? Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release date:")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Genres:")]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        public EditSystemRequirementsVm MinimalSystemRequirements { get; set; }

        [Required]
        public EditSystemRequirementsVm RecommendedSystemRequirements { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReleaseDate > DateTime.Now)
            {
                yield return new ValidationResult("Date must me less than current date");
            }

            if (!GenreIds.Any())
            {
                yield return new ValidationResult("Game must have at least one genre");
            }
        }
    }
}