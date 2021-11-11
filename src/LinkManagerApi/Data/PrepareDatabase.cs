using System;
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
        if (!isDevelopment)
        {
            try{
                Console.WriteLine("Migration");
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Migration= {e.Message}");
            }
        }
    }
}