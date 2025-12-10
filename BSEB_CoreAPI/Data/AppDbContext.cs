using BSEB_CoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BSEB_CoreAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CollegeUser> CollegeUsers { get; set; }
        public DbSet<LoginUserInfo> LoginUserInfo { get; set; }
        public DbSet<CollegeMst> CollegeMst { get; set; }
        public DbSet<Student_Mst> Student_Mst { get; set; }
        public DbSet<Faculty_Mst> Faculty_Mst { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            // If "Users" is a DTO or view
            modelBuilder.Entity<CollegeMst>().HasNoKey();
            modelBuilder.Entity<LoginUserInfo>().HasNoKey();
            modelBuilder.Entity<Student_Mst>().HasKey(x => x.StudentID);
            modelBuilder.Entity<Faculty_Mst>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
