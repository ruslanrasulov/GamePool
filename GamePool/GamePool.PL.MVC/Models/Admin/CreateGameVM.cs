using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GamePool.PL.MVC.Models.Admin
{
    public class CreateGameVM : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public IEnumerable<int> GenreIds { get; set; }

        [Required]
        public CreateSystemRequirementsVM MinimalSystemRequirements { get; set; }

        [Required]
        public CreateSystemRequirementsVM RecommendedSystemRequirements { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReleaseDate > DateTime.Now)
            {
                yield return new ValidationResult("Date must me less than current date");
            }

            if (GenreIds.Count() == 0)
            {
                yield return new ValidationResult("Game must have at least one genre");
            }
        }
    }
}