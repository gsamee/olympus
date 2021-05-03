using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservationHandlingWeb.Models;
using ReservationHandlingWeb.ViewModels;

namespace ReservationHandlingWeb.Data
{
    public class ReservationDBContext : DbContext
    {
        public ReservationDBContext(DbContextOptions<ReservationDBContext> options) : base(options)
        {
        }

        public DbSet<UserMain> UserMain { get; set; }
        public DbSet<HallDetail> HallDetail { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<EventSetup> EventSetup { get; set; }
        public DbSet<MemberDetails> MemberDetails { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<SP_MealDet> SP_MealDet { get; set; }

        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMain>().ToTable("UserMain").HasKey("ID");
            modelBuilder.Entity<HallDetail>().ToTable("HallDetail").HasKey("ID");
            modelBuilder.Entity<EventType>().ToTable("EventType").HasKey("ID");
            modelBuilder.Entity<EventSetup>().ToTable("EventSetup").HasKey("ID");
            modelBuilder.Entity<MemberDetails>().ToTable("MemberDetails").HasKey("ID");
            modelBuilder.Entity<Attendance>().ToTable("Attendance").HasKey("ID");
            modelBuilder.Entity<SP_MealDet>().HasNoKey();
        }

        public void DeleteMembers(int uid)
        {
            var p1 = new SqlParameter("@ID", uid);
            Database.ExecuteSqlRaw("exec SP_Delete @ID", p1);
        }
    }
}
