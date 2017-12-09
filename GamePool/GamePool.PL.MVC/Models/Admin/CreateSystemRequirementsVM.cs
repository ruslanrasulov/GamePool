using System.ComponentModel.DataAnnotations;

namespace GamePool.PL.MVC.Models.Admin
{
    public class CreateSystemRequirementsVM
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(100)]
        public string Processor { get; set; }

        [Required]
        [MaxLength(100)]
        public string Memory { get; set; }

        [Required]
        [MaxLength(100)]
        public string OperationSystem { get; set; }

        [Required]
        [MaxLength(100)]
        public string Graphics { get; set; }

        [Required]
        [MaxLength(100)]
        public string Storage { get; set; }

        [Required]
        [MaxLength(50)]
        public string DirectX { get; set; }
    }
}