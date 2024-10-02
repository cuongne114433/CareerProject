using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CareerProject.Models.DTO
{
    public partial class CareerDBContext : DbContext
    {
        public CareerDBContext()
            : base("name=CareerDBContext1")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbl_ApplyJob> tbl_ApplyJob { get; set; }
        public virtual DbSet<tbl_Category> tbl_Category { get; set; }
        public virtual DbSet<tbl_Company> tbl_Company { get; set; }
        public virtual DbSet<tbl_CV> tbl_CV { get; set; }
        public virtual DbSet<tbl_Job> tbl_Job { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_ApplyJob>()
                .Property(e => e.Mail)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Category>()
                .HasMany(e => e.tbl_Job)
                .WithRequired(e => e.tbl_Category)
                .HasForeignKey(e => e.IDCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Company>()
                .Property(e => e.PhoneNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Company>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Company>()
                .Property(e => e.PassWord)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Company>()
                .HasMany(e => e.tbl_Job)
                .WithRequired(e => e.tbl_Company)
                .HasForeignKey(e => e.IDCompany);

            modelBuilder.Entity<tbl_Job>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Job>()
                .HasMany(e => e.tbl_ApplyJob)
                .WithRequired(e => e.tbl_Job)
                .HasForeignKey(e => e.IDJob);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.PassWord)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_ApplyJob)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.IDUser);

            modelBuilder.Entity<tbl_User>()
                .HasMany(e => e.tbl_CV)
                .WithRequired(e => e.tbl_User)
                .HasForeignKey(e => e.IDUser);
        }
    }
}
