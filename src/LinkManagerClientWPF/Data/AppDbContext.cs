using Microsoft.EntityFrameworkCore;
using LinkManagerClientWPF.Entities;
using System.IO;
using System;
using BespokeFusion;

namespace LinkManagerClientWPF.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }
    public DbSet<Link> Links { get; set; }
    public DbSet<LinkVisitCount> VisitsCount { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string currentPath = Environment.CurrentDirectory.ToString();
        string path = Path.Combine(currentPath, "LinkBankLocalDB.db");
        try
        {
            optionsBuilder.UseSqlite("Data Source=" + path);
        }
        catch
        {
            MaterialMessageBox.ShowError("sqlite database not found!");
        }
    }
}