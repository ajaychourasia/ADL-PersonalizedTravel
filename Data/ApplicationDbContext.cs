using System;
using System.Collections.Generic;
using System.Text;
using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADL.PersonalizedTravel.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ADL.PersonalizedTravel.Models.TourActivity> TourActivity { get; set; }
    }
}
