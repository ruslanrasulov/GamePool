using System.Collections.Generic;

namespace GamePool.PL.MVC.Models.Admin
{
    public class UserListItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> AvailableRoles { get; set; }

        public IEnumerable<string> CurrentRoles { get; set; }
    }
}