using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Priyarank.Models;

namespace Priyarank.Data
{
    public class RankContext : DbContext
    {
        //public RankContext(){}

        public RankContext(DbContextOptions<RankContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Team>()
                .Property(t => t.Elo)
                .HasDefaultValue(1200);
            builder.Entity<Match>()
                .Property(m => m.created)
                .HasDefaultValue(DateTime.Now);
            builder.Entity<Match>()
                .Property(m => m.Id)
                .HasDefaultValue(Guid.NewGuid());
            builder.Entity<Team>()
                .Property(t => t.Id)
                .HasDefaultValue(Guid.NewGuid());
        }


        //public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }
    }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                //{
                //    if (!optionsBuilder.IsConfigured)
                //    {
                //        IConfigurationRoot configuration = new ConfigurationBuilder()
                //           .SetBasePath(Directory.GetCurrentDirectory())
                //           .AddJsonFile("appsettings.json")
                //           .Build();
                //        var connectionString = configuration.GetConnectionString("RankContext");
                //        optionsBuilder.UseSqlServer(connectionString);
                //    }
                //}
}
        
