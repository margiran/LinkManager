using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagerClientWPF.Data;
using LinkManagerClientWPF.Entities;
using LinkManagerClientWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkManagerClientWPF.Services;

public class LinkLocalDbService : ILinkLocalDbService
{
    private readonly AppDbContext _context;
    public LinkLocalDbService(AppDbContext context)
    {
        _context = context;
    }

    public void AddOrUpdate(List<Link> linksToUpdate)
    {
        _context.ChangeTracker.Clear();

        List<Link> ToUpdate = new();
            foreach (var link in linksToUpdate)
            {
            if (LinkExist(link.Id))
            {
                _context.Links.Update(link);
            }
            else
                _context.Links.Add(link);
            }
            _context.SaveChanges();

    }
    private  bool LinkExist(Guid id)
    {
           return _context.Links.Any(l => l.Id == id);

        }
        public IEnumerable<LinkModel> GetAll(string filter = "")
    {
        IEnumerable<Link> links;

            //links = _context.Links;

            if (!string.IsNullOrEmpty(filter))
            {
                links = _context.Links.Where(l => l.Title.Contains(filter) || l.ShortDescription.Contains(filter)).ToList();
            }
            else
            {
                links = _context.Links.ToList();
            }

            return links.Select(m => new LinkModel
            {
                Title = m.Title ?? "",
                ShortDescription = m.ShortDescription ?? "",
                Argument = m.Argument ?? "",
                DefaultPassword = m.DefaultPassword ?? "",
                Description = m.Description?? "",
                FileName = m.FileName??"",
                Id = m.Id,
                InternetNeeded = m.InternetNeeded,
                LinkVisitCount = m.VisitedCount,
                Order = m.Order,
                UserName = m.UserName??""

            }).OrderBy(l => l.Order).ToList();
    }

    public Link GetById(Guid id)
    {

        return _context.Links.FirstOrDefault(l => l.Id == id);

    }

    public string GetLastUpdated()
    {
        string lastUpdate="";
 
            var links = _context.Links.ToList();
            Link link;
            //link= _context.Links.OrderByDescending(l=> l.UpdateAt).First();
            if (!links.Any())
                return lastUpdate;
            link = links.OrderByDescending(l => l.UpdateAt).First();
            //if(link!= null)
            lastUpdate = link.UpdateAt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff'Z'");
            return lastUpdate;
        
    }
    public void SetVisited(Guid id)
    {

            var existing = _context.Links.FirstOrDefault(v=> v.Id== id);
            if (existing != null)
            {
                existing.VisitedCount += 1;

                _context.Links.Update(existing);
            }
            _context.SaveChanges();
        }
}