using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.Common.Entities
{
    public sealed class GameEntity
    {
        public int Id { get; set; }

        public int? AvatarId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<int> ScreenshotIds { get; set; }

        public IEnumerable<string> Genres { get; set; }
    }
}