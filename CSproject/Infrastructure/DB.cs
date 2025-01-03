﻿using CSproject.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSproject.Infrastructure
{

    public class EFContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Connection> Connections => Set<Connection>();

        public EFContext(){
            //На время разработки 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=agregat.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User {  Name = "Admin", Gender = "m" , Login = "admin" , Password = "1111" },
                    new User {  Name = "AdminHost", Gender = "m", Login = "adminHost", Password = "1111" }
            );
        }
    }
}
