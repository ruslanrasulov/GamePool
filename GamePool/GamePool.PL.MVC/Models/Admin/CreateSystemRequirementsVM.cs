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
        [Display(Name = "Processor:")]
        public string Processor { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Memory:")]
        public string Memory { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Operation system:")]
        public string OperationSystem { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Graphics:")]
        public string Graphics { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Storage:")]
        public string Storage { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "DirectX:")]
        public string DirectX { get; set; }
    }
}