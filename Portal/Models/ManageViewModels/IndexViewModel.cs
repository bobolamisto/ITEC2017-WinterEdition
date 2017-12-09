using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = " Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Radius of interest (km)")]
        public double RadiusOfInterest { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(14, 150)]
        public int Age { get; set; }

        public string StatusMessage { get; set; }
        
        public int ProfilePictureId { get; set; }

        [Display(Name = "Choose Profile Picture")]
        public List<IFormFile> Image { get; set; }

        public IndexViewModel()
        {
            Image = new List<IFormFile>();
        }
    }
}
