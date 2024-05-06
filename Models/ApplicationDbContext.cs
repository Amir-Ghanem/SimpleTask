using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SimpleTask.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.CreatedBy)
                .WithMany(u => u.CreatedEmployees)
                .HasForeignKey(e => e.CreatedByID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ModifiedBy)
                .WithMany(u => u.ModifiedEmployees)
                .HasForeignKey(e => e.ModifiedByID)
                .OnDelete(DeleteBehavior.Restrict);




            //modelBuilder.Entity<Department>().HasData(
            //    new Department { ID = 1, Name = "IT", Description = "Information Technology" },
            //    new Department { ID = 2, Name = "HR", Description = "Human Resources" }
            //    );

       
            //modelBuilder.Entity<User>().HasData(
            //    new User { ID = 1, Name = "Admin", Email = "admin@admin.com", Password = "admin" }
            //    );
        }


        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department { Name = "HR", Description = "Description of HR Department" },
                        new Department { Name = "IT", Description = "Description of Information Technology Department" },
                        new Department { Name = "Architecure", Description = "Description of Architecure Department" },
                        new Department { Name = "Accounting", Description = "Description of Accounting Department" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { Name = "Amir", Email = "amir@gmail.com", Password = "123456" },
                        new User { Name = "Tarek", Email = "tarek@gmail.com", Password = "123456" },
                        new User { Name = "Khaled", Email = "khaled@gmail.com", Password = "123456" },
                        new User { Name = "Fares", Email = "fares@gmail.com", Password = "123456" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
