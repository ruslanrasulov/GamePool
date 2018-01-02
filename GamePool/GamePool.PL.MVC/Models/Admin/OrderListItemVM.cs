namespace GamePool.PL.MVC.Models.Admin
{
    public class OrderListItemVm
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