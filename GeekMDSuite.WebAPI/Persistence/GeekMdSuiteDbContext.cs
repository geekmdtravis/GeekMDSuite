﻿using GeekMDSuite.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekMDSuite.WebAPI.Persistence
{
    public class GeekMdSuiteDbContext : DbContext
    {
        public GeekMdSuiteDbContext(DbContextOptions<GeekMdSuiteDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PatientEntity>().OwnsOne(p => p.Name);
            modelBuilder.Entity<PatientEntity>().OwnsOne(p => p.Gender);
            modelBuilder.Entity<AudiogramEntity>().OwnsOne(agDataset => agDataset.Left, dataset =>
            {
                dataset.OwnsOne(f => f.F125);
                dataset.OwnsOne(f => f.F250);
                dataset.OwnsOne(f => f.F500);
                dataset.OwnsOne(f => f.F1000);
                dataset.OwnsOne(f => f.F2000);
                dataset.OwnsOne(f => f.F3000);
                dataset.OwnsOne(f => f.F4000);
                dataset.OwnsOne(f => f.F6000);
                dataset.OwnsOne(f => f.F8000);
            });
            modelBuilder.Entity<AudiogramEntity>().OwnsOne(agDataset => agDataset.Right, dataset =>
            {
                dataset.OwnsOne(f => f.F125);
                dataset.OwnsOne(f => f.F250);
                dataset.OwnsOne(f => f.F500);
                dataset.OwnsOne(f => f.F1000);
                dataset.OwnsOne(f => f.F2000);
                dataset.OwnsOne(f => f.F3000);
                dataset.OwnsOne(f => f.F4000);
                dataset.OwnsOne(f => f.F6000);
                dataset.OwnsOne(f => f.F8000);
            });
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var test = new AudiogramEntity();
            optionsBuilder.UseSqlite("Data Source=context.db");
        }
        
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<AudiogramEntity> Audiograms { get; set; }
    }
}