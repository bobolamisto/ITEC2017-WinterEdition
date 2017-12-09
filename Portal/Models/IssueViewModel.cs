using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public User LoggedUser { get; set; }
    }
}
