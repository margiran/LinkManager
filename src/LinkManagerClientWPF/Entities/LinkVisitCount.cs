using System;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerClientWPF.Entities;

public class LinkVisitCount
{
    public LinkVisitCount()
    {
        
    }
    [Key]
    public Guid Id { get; set; }

    public int VisitedCount { get; set; }= 0;

    public Link Link { get; set; }

}
