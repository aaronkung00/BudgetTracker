using AaronKung.BudgetTracker.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AaronKung.BudgetTracker.Infrastructure.Data
{
    public class BudgetTrackerDbContext : DbContext
    {
        /*----------- Initialize DbSet to DbContext -------------*/
        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }

        /*--------------------------------------------------------*/
        public BudgetTrackerDbContext(DbContextOptions<BudgetTrackerDbContext> options) : base(options)
        {

        }

        /* --- Using Fluent API ---*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUsers);
            modelBuilder.Entity<Expenditure>(ConfigureExpenditure);
            modelBuilder.Entity<Income>(ConfigureIncomes);
        }

        /*--- Configuration of Tables --*/

        private void ConfigureUsers(EntityTypeBuilder<User> builder) 
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).HasColumnType("varchar").IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.Property(u => u.Password).HasColumnType("varchar").IsRequired();
            builder.Property(u => u.Password).HasMaxLength(10);
            builder.Property(u => u.FullName).HasColumnType("varchar");
            builder.Property(u => u.FullName).HasMaxLength(50);
 
            
        }
        private void ConfigureExpenditure(EntityTypeBuilder<Expenditure> builder)
        {
            builder.ToTable("Expenditures");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Amount).HasColumnType("money").IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar");
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e => e.Remarks).HasColumnType("varchar");
            builder.Property(e => e.Remarks).HasMaxLength(500);
            // one to many relationship between user and expenditure
            builder.HasOne(e => e.User).WithMany(e => e.Expenditures).HasForeignKey(e => e.UserId);
        }
        private void ConfigureIncomes(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Incomes");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Amount).HasColumnType("money").IsRequired();
            builder.Property(i => i.Description).HasColumnType("varchar");
            builder.Property(i => i.Description).HasMaxLength(100);
            builder.Property(i => i.Remarks).HasColumnType("varchar");
            builder.Property(i => i.Remarks).HasMaxLength(500);
            // one to many relationship between user and Incomes
            builder.HasOne(i => i.User).WithMany(i => i.Incomes).HasForeignKey(i => i.UserId);
        }

    }
}
