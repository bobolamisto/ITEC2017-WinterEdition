using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Data
{
    public static class DbInitializer
    {

        public static void Initialize(PortalDbContext context)//SchoolContext is EF context
        {

            context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

            // Look for any students.
            if (context.Locations.Any())
            {
                return;   // DB has been seeded
            }

            //var locations = new Location[]
            //{
            //    new Location{Id = 1, Latitude = 45.747119, Longitude=21.231615},
            //    new Location{Id = 2,Latitude = 45.748965, Longitude=21.227485},
            //    new Location{Id = 3,Latitude = 45.746315, Longitude=21.220769},
            //    new Location{Id = 4,Latitude = 45.735792, Longitude=21.251111},
            //    new Location{Id = 5,Latitude = 45.719825, Longitude=21.237979},
            //    new Location{Id = 6,Latitude = 45.715525, Longitude=21.227229},
            //    new Location{Id = 7,Latitude = 45.724484, Longitude=21.167877},
            //    new Location{Id = 8,Latitude = 45.754017, Longitude=21.225833},
            //    new Location{Id = 9,Latitude = 45.751816, Longitude=21.237943},
            //    new Location{Id = 10,Latitude = 45.742772, Longitude=21.233308},
            //    new Location{Id = 11,Latitude = 45.785485, Longitude=21.309285},
            //    new Location{Id = 12,Latitude = 45.818688, Longitude=21.270430},
            //};
            //foreach(var loc in locations)
            //{
            //    context.Locations.Add(loc);
            //}
            //context.SaveChanges();

        }
    }
}
