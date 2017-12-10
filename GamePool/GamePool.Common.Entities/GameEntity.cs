using System;

namespace GamePool.Common.Entities
{
    public sealed class GameEntity
    {
        public int Id { get; set; }

        public int? AvatarId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal? Rating { get; set; }

        public SystemRequirements MinimalSystemRequirements { get; set; }

        public SystemRequirements RecommendedSystemRequirements { get; set; }
    }
}