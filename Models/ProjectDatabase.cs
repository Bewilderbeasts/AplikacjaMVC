using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacjaMVC.Models;

namespace AplikacjaMVC.Models
{
    public class ProjectDatabase :DbContext
    {
        public ProjectDatabase(DbContextOptions<ProjectDatabase> options) : base(options)
        {

        }
        public DbSet<Register> Register { get; set; }
        public DbSet<AplikacjaMVC.Models.Images> Images { get; set; }

        public DbSet<ImagesVotes> ImagesVotes { get; set; }

    }
}
