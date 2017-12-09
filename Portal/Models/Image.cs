using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public int? IssueId { get; set; }
        public virtual Issue Issue { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}
