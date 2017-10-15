using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.Common.Entities
{
    public sealed class ImageEntity
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        public string AlternativeText { get; set; }
    }
}