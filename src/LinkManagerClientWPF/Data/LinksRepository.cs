using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagerClientWPF.Entities;

namespace LinkManagerClientWPF.Data;

public class LinksRepository : ILinkRepository
{
    public void AddOrUpdate(List<Link> linksToUpdate)
    {
        using (var db = new AppDbContext())
        {
            db.Links.UpdateRange(linksToUpdate);
            db.SaveChanges();
        }
    }

    public IEnumerable<Link> GetAll(bool orderByVisitCount=true, string filter="")
    {
        IEnumerable<Link> links;
        using (var db = new AppDbContext())
        {
            links= db.Links;
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
        }
        return links.ToList();
    }

    public Link GetById(Guid id)
    {
        Link link;
        using (var db = new AppDbContext())
        {
            link= db.Links.FirstOrDefault(l=> l.Id == id);
        }
        return link;
    }

    public string GetLastUpdated()
    {
        string lastUpdate="";
        Link link;
        using (var db = new AppDbContext())
        {
            link= db.Links.OrderByDescending(l=> l.UpdateAt).First();
        }
        if(link!= null)
            lastUpdate= link.UpdateAt.ToString();
        return lastUpdate;
    }
    public void SetVisited(Guid id)
    {
        LinkVisitCount visitCount=new LinkVisitCount{
            Id=id,
            VisitedCount=1
        };
        using (var db = new AppDbContext())
        {
            var existing= db.VisitsCount.Find(id);
            if (existing!=null)
                visitCount.VisitedCount=existing.VisitedCount+1; 
            db.VisitsCount.Update(visitCount);
            db.SaveChanges();
        }
    }
}