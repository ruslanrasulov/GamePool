namespace GamePool.PL.MVC.Models.Product
{
    public class OrderedGameVm
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int? AvatarId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}