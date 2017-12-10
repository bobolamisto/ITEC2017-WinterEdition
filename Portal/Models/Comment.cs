using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
        public string Text { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        public Comment()
        {

        }
    }
}
