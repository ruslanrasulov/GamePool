namespace GamePool.PL.MVC.Models.Admin
{
    public class CreateSystemRequirementsVM
    {
        public string Type { get; set; }

        public string Processor { get; set; }

        public string Memory { get; set; }

        public string OperationSystem { get; set; }

        public string Graphics { get; set; }

        public string Storage { get; set; }

        public string DirectX { get; set; }
    }
}