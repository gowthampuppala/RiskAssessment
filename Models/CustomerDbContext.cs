using Microsoft.EntityFrameworkCore;
using RiskAssessment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskAssessment.Models
{
    public class CustomerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=KEERTHANA;Initial Catalog=CustomerDB;Integrated Security=True");
        }
        public DbSet<CollateralRisk> collateralRisks { get; set; }
        public DbSet<CurrentValue> currentValues { get; set; }
        public DbSet<Collateral> collateral { get; set; }
    }
}
