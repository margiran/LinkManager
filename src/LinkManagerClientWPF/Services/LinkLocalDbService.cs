using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagerClientWPF.Data;
using LinkManagerClientWPF.Entities;
using LinkManagerClientWPF.Models;

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
        foreach (var link in linksToUpdate)
        {
            _context.Links.Update(link);
            //_context.Links.UpdateRange(linksToUpdate);
            _context.SaveChanges();
        }
    }

    public IEnumerable<LinkModel> GetAll(bool orderByVisitCount=true, string filter="")
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
        return links.Select( m => new LinkModel
        {
            Title = m.Title,
            ShortDescription = m.ShortDescription,
            Argument = m.Argument,
            DefaultPassword = m.DefaultPassword,
            Description = m.Description,
            FileName = m.FileName,
            Id = m.Id,  
            InternetNeeded = m.InternetNeeded,  
            LinkVisitCount=m.LinksVisitCount.VisitedCount,
            Order=m.Order,  
            UserName=m.UserName
            
        }).ToList();
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

        var links= _context.Links.ToList();
        Link link;
            //link= _context.Links.OrderByDescending(l=> l.UpdateAt).First();
        if (!links.Any())
            return lastUpdate;
        link = links.OrderByDescending(l=> l.UpdateAt).First();
        //if(link!= null)
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