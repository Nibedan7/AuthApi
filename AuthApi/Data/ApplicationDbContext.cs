using AuthApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "John Doe", Designation = "Software Engineer", Department = "IT", Joining_date = new DateOnly(2020, 5, 15) },
                    new Employee { Id = 2, Name = "Jane Smith", Designation = "HR Manager", Department = "HR", Joining_date = new DateOnly(2018, 3, 22) },
                    new Employee { Id = 3, Name = "Alice Brown", Designation = "Marketing Specialist", Department = "Marketing", Joining_date = new DateOnly(2021, 8, 10) },
                    new Employee { Id = 4, Name = "Bob Johnson", Designation = "Product Manager", Department = "Product", Joining_date = new DateOnly(2017, 11, 1) },
                    new Employee { Id = 5, Name = "Charlie Davis", Designation = "QA Engineer", Department = "IT", Joining_date = new DateOnly(2019, 6, 30) },
                    new Employee { Id = 6, Name = "David Wilson", Designation = "Business Analyst", Department = "Business", Joining_date = new DateOnly(2020, 2, 5) },
                    new Employee { Id = 7, Name = "Emily Clark", Designation = "Sales Executive", Department = "Sales", Joining_date = new DateOnly(2022, 7, 20) },
                    new Employee { Id = 8, Name = "Frank Harris", Designation = "Systems Architect", Department = "IT", Joining_date = new DateOnly(2016, 4, 18) },
                    new Employee { Id = 9, Name = "Grace Lewis", Designation = "Customer Support", Department = "Support", Joining_date = new DateOnly(2021, 1, 12) },
                    new Employee { Id = 10, Name = "Henry Walker", Designation = "UI/UX Designer", Department = "Design", Joining_date = new DateOnly(2019, 9, 25) }

            );
        }
    }
}
