using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public enum StateType
    {
       Active,
       Archived,
       Solved
    }
    public class IssueState
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public StateType Type { get; set; }
        public IssueState()
        {

        }
    }
}
