namespace GamePool.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int GameId { get; set; }

        public int Quantity { get; set; }
    }
}