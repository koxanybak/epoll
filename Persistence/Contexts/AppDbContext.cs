using Microsoft.EntityFrameworkCore;
using epoll.Domain.Models;
using System.Collections.Generic;

namespace epoll.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSnakeCaseNamingConvention(); // Converts table/column names from 'TableName' to 'table_name'

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Poll>().ToTable("poll");
            modelBuilder.Entity<Poll>().HasKey(p => p.Id);
            modelBuilder.Entity<Poll>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Poll>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Poll>().HasMany(p => p.Options).WithOne(p => p.Poll).HasForeignKey(p => p.PollId);

    
            modelBuilder.Entity<PollOption>().ToTable("poll_option");
            modelBuilder.Entity<PollOption>().HasKey(p => p.Id);
            modelBuilder.Entity<PollOption>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<PollOption>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<PollOption>().Property(p => p.Votes).IsRequired().HasDefaultValue(0);
        }
    }
}