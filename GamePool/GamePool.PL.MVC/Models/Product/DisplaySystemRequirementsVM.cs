using System.ComponentModel.DataAnnotations;

namespace GamePool.PL.MVC.Models.Product
{
    public class DisplaySystemRequirementsVm
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string Type { get; set; }

        [Display(Name = "Processor:")]
        public string Processor { get; set; }

        [Display(Name = "Memory:")]
        public string Memory { get; set; }

        [Display(Name = "OS:")]
        public string OperationSystem { get; set; }

        [Display(Name = "Graphics:")]
        public string Graphics { get; set; }

        [Display(Name = "Storage:")]
        public string Storage { get; set; }

        [Display(Name = "DirectX:")]
        public string DirectX { get; set; }
    }
}