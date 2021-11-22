using Microsoft.EntityFrameworkCore;
using LinkManagerClientWPF.Entities;
using System.IO;
using System;
using BespokeFusion;

namespace LinkManagerClientWPF.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ): base(options)
    {
        Database.EnsureCreated();

    }
    public DbSet<Link> Links { get; set; }
    public DbSet<LinkVisitCount> VisitsCount { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Link>()
            .HasOne(p => p.LinksVisitCount)
            .WithOne(p => p.Link)
            .HasForeignKey<LinkVisitCount>(p=>p.LinkId);
        modelBuilder.Entity<LinkVisitCount>()
            .HasOne(p => p.Link)
            .WithOne(p => p.LinksVisitCount)
            .HasForeignKey<Link>(p => p.Id);
        modelBuilder.Entity<Link>()
            .HasQueryFilter(l => !l.IsDelete);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //string currentPath = Environment.CurrentDirectory.ToString();
        //string path = Path.Combine(currentPath, "LinkBankLocalDB.db");
        //try
        //{
        //    optionsBuilder.UseSqlite("Data Source=" + path);
        //}
        //catch
        //{
        //    MaterialMessageBox.ShowError("sqlite database not found!");
        //}
    }
}