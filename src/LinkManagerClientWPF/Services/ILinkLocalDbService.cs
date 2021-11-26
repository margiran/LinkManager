using System;
using System.Collections.Generic;
using LinkManagerClientWPF.Entities;
using LinkManagerClientWPF.Models;

namespace LinkManagerClientWPF.Services;

public interface ILinkLocalDbService
{
    IEnumerable<LinkModel> GetAll(bool orderByVisitCount=true,string filter="");

    Link GetById(Guid id);

    string GetLastUpdated();
    void SetVisited(Guid id);

    void AddOrUpdate(List<Link> linksToUpdate);

}
