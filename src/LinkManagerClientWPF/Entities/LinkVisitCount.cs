using System;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerClientWPF.Entities;

public class LinkVisitCount
{
    public LinkVisitCount()
    {
        
    }
    [Key]
    public int Id { get; set; }
    public Guid LinkId { get; set; }

    public int VisitedCount { get; set; }= 0;

    public Link Link { get; set; }

}
