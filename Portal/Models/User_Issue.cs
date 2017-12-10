using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public enum VoteType
    {
        Upvote,
        Downvote,
        None
    }
    public class User_Issue
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IssueId { get; set; }
        public bool IsAuthor { get; set; }
        public VoteType Vote { get; set; }
        public virtual User User { get; set; }
        public virtual Issue Issue { get; set; }
        

    }
}
