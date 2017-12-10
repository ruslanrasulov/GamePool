namespace GamePool.Common.Entities
{
    public sealed class SystemRequirements
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string Type { get; set; }

        public string Processor { get; set; }

        public string Memory { get; set; }

        public string OperationSystem { get; set; }

        public string Graphics { get; set; }

        public string Storage { get; set; }

        public string DirectX { get; set; }
    }
}