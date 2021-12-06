using System;
using System.Collections.Generic;
using LinkManagerApi.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.Data;

public static class PrepareDatabase
{
    public static void PrepareDB(IApplicationBuilder app, bool isDevelopment)
    {
        using (var scopedService = app.ApplicationServices.CreateScope())
        {
            SeedData(scopedService.ServiceProvider.GetService<ApplicationDbContext>(),isDevelopment );
        }
    }
    private static void SeedData(ApplicationDbContext context, bool isDevelopment)
    {
        // if (!isDevelopment)
        // {
            try{
                Console.WriteLine("Migration");
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Migration= {e.Message}");
            }
        // }
        List<Link> sampleData= new List<Link>{
            new Link { Title="Google" ,FileName="chrome",Argument="google.com",ShortDescription="google search",Description="description",Order=1 },            
            new Link { Title="Yahoo" ,FileName=@"C:\Program Files\Mozilla Firefox\firefox.exe",Argument="yahoo.com",ShortDescription="yahoo",Order=2 },            
            new Link { Title="Word" ,FileName=@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE",Argument="newdoc.docx",ShortDescription="word",Order=3 },            
        };
        context.Links.AddRange(sampleData);
        context.SaveChanges();
    }
}