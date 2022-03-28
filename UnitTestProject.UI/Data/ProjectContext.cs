using Microsoft.EntityFrameworkCore;
using UnitTestProject.UI.Entities;

namespace UnitTestProject.UI.Data
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product(){Id=1,Name="Product1",Stock=100,Price=1000},
                new Product(){Id=2,Name="Product2",Stock=100,Price=2000},
                new Product(){Id=3,Name="Product3",Stock=100,Price=3000},
                new Product(){Id=4,Name="Product4",Stock=100,Price=4000},
                new Product(){Id=5,Name="Product4",Stock=100,Price=5000},
                new Product(){Id=6,Name="Product5",Stock=100,Price=6000},
                new Product(){Id=7,Name="Product6",Stock=100,Price=7000},
                new Product(){Id=8,Name="Product7",Stock=100,Price=8000},
                new Product(){Id=9,Name="Product8",Stock=100,Price=9000},



            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
