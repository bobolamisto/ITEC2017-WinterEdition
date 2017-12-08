using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<IssueState> States { get; set; }
        public virtual List<User_Issue> User_Issues { get; set; }

        public Issue()
        {
            User_Issues = new List<User_Issue>();
            States = new List<IssueState>();

        }
        //to do: pictures


    }
}
