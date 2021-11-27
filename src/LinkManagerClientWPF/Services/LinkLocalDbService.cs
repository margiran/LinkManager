using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagerClientWPF.Data;
using LinkManagerClientWPF.Entities;
using LinkManagerClientWPF.Models;

namespace LinkManagerClientWPF.Services;

public class LinkLocalDbService : ILinkLocalDbService
{
    private readonly AppDbContextFactory _contextFactory;
    public LinkLocalDbService(AppDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public void AddOrUpdate(List<Link> linksToUpdate)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
            foreach (var link in linksToUpdate)
            {
                if (LinkExist(link.Id))
                    _context.Links.Update(link);
                else
                    _context.Links.Add(link);
            }
            _context.SaveChanges();

        }
    }

    private bool LinkExist(Guid id)
    {
        using (var _context = _contextFactory.CreateDbContext())
        {
           return _context.Links.Any(l => l.Id == id);
        }

        }
        public IEnumerable<LinkModel> GetAll(string filter = "")
    {
        IEnumerable<Link> links;
        using (var _context = _contextFactory.CreateDbContext())
        {

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
    }

    public Link GetById(Guid id)
    {

        Link? link;
        using (var _context = _contextFactory.CreateDbContext())
        {

            link = _context.Links.FirstOrDefault(l => l.Id == id);

        }
        return link;
    }

    public string GetLastUpdated()
    {
        string lastUpdate="";
        using (var _context = _contextFactory.CreateDbContext())
        {

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
    }
    public void SetVisited(Guid id)
    {
        using (var _context = _contextFactory.CreateDbContext())
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
}