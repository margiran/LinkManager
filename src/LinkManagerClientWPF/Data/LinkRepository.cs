using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagerClientWPF.Entities;

namespace LinkManagerClientWPF.Data;

public class LinkRepository : ILinkRepository
{
    private readonly AppDbContext _context;
    public LinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddOrUpdate(List<Link> linksToUpdate)
    {
            _context.Links.UpdateRange(linksToUpdate);
            _context.SaveChanges();
    }

    public IEnumerable<Link> GetAll(bool orderByVisitCount=true, string filter="")
    {
        IEnumerable<Link> links;
            links= _context.Links;
            if(!string.IsNullOrEmpty(filter))
            {
                links.Where(l=> l.Title.Contains(filter) || l.ShortDescription.Contains(filter));
            }
            if(orderByVisitCount)
            {
                links.OrderByDescending(l=> l.LinksVisitCount.VisitedCount);
            }
            else
            {
                links.OrderBy(l=> l.Order);
            }
        return links.ToList();
    }

    public Link GetById(Guid id)
    {
        Link link;
            link= _context.Links.FirstOrDefault(l=> l.Id == id);
        return link;
    }

    public string GetLastUpdated()
    {
        string lastUpdate="";
        Link link;
            link= _context.Links.OrderByDescending(l=> l.UpdateAt).First();
        if(link!= null)
            lastUpdate= link.UpdateAt.ToString();
        return lastUpdate;
    }
    public void SetVisited(Guid id)
    {
        LinkVisitCount visitCount=new LinkVisitCount{
            LinkId=id,
            VisitedCount=1
        };
            var existing= _context.VisitsCount.Find(id);
            if (existing!=null)
                visitCount.VisitedCount=existing.VisitedCount+1; 
            _context.VisitsCount.Update(visitCount);
            _context.SaveChanges();
    }
}