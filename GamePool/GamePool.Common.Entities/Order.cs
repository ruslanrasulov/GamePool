using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.Common.Entities
{
    public sealed class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string State { get; set; }

        public int? UserId { get; set; }

        public int? GameId { get; set; }
    }
}