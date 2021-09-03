using ElenaDalSavio_Prova6.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaDalSavio_Prova6.EF
{
    public class PoliciesContext:DbContext
    {
        public DbSet<Core.Models.Client> Clients { get; set; }

        public DbSet<Policy> Policies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;
		Database=AgenziaAssicurazione;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core.Models.Client>().HasData(new Core.Models.Client
            {
                Id = 1,
                Code="DLSLNE98A88G687Y",
                Name = "Elena",
                LastName="Dal Savio"
            },
            new Core.Models.Client
            {
                Id = 2,
                Code = "RSSALS55A88G296Y",
                Name = "Alessio",
                LastName = "Rossi"
            },
            new Core.Models.Client
            {
                Id = 3,
                Code = "FRCBRN87A82G687Y",
                Name = "Francesca",
                LastName = "Bruni"
            },
            new Core.Models.Client
            {
                Id = 4,
                Code = "RBRZNN98A88H687Y",
                Name = "Roberto",
                LastName = "Zanni"
            });
        }
    }
}
