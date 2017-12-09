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
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
        public IssueState()
        {

        }
    }
}
