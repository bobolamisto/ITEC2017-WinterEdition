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
            if (context.Images.Any())
            {
                return;   // DB has been seeded
            }


        }
    }
}
