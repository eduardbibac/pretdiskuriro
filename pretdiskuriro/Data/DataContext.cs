﻿using Microsoft.EntityFrameworkCore;
using System;

namespace DbModels

    //
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DailyPrice> DailyPrices { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MarketProduct> MarketProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\ed\Documents\pretdiskuriro\pretdiskuriro\db.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make fields UNIQUE here because microsoft is too retarded to make proper convenience with annotations actually fukcing work
            // Multi dolar company
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Email })
                .IsUnique(true);

            // Many to many junction table
            modelBuilder.Entity<MarketProduct>().HasKey(t => new { t.MarketId, t.ProductId });

            //modelBuilder.Entity<StudentCourse>()
            //    .HasOne<Course>(sc => sc.Course)
            //    .WithMany(s => s.StudentCourses)
            //    .HasForeignKey(sc => sc.CId);
        }
    }
}