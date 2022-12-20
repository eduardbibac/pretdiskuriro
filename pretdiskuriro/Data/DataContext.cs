﻿using Microsoft.EntityFrameworkCore;

namespace DbModels
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\ed\Documents\pretdiskuriro\pretdiskuriro\db.db");
    }
}