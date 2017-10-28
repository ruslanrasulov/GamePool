using System;

namespace GamePool.Common.Entities
{
    public sealed class Rating
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public int? GameId { get; set; }

        public int? UserId { get; set; }
    }
}