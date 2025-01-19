using CSproject.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSproject.Infrastructure
{

    public class EFContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Connection> Connections => Set<Connection>();
        public DbSet<Message> Messages => Set<Message>();

        public DbSet<EventMessage> EventMessages => Set<EventMessage>();
        public DbSet<TrainingMessage> TrainMessages => Set<TrainingMessage>();


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
                    new User {  Name = "Admin", Sex = "m" , Login = "admin" , Password = "1111" , Photo="" },
                    new User { 
                        Name = "AdminHost", 
                        Sex = "m",
                        Login = "adminHost",
                        Password = "1111" ,
                        Photo = "https://avatars.mds.yandex.net/i?id=39fd21ed663a73cc3e4e320cc959ea09_l-5227258-images-thumbs&n=13" }
            );
        }
    }
}
