using System;
using Makarevich_sol_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Makarevich_sol_API.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> opt):base(opt)
        {
            
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
    }
}
