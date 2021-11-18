using System;
using System.Collections.Generic;
using LinkManagerClientWPF.Entities;

namespace LinkManagerClientWPF.Data;

public interface ILinkRepository
{
    IEnumerable<Link> GetAll(bool orderByVisitCount=true,string filter="");

    Link GetById(Guid id);

    string GetLastUpdated();
    void SetVisited(Guid id);

    void AddOrUpdate(List<Link> linksToUpdate);

}
