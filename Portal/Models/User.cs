using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Portal.Models
{
    public enum Gender
    {
        Female,
        Male,
        Other
    }

    public enum UserStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        //to do: remove optional
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public double RadiusOfInterest { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public UserStatus Status { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<User_Issue> User_Issues { get; set; }

        public User()
        {
            User_Issues = new List<User_Issue>();
        }


        //to do: add pictures

    }
}
