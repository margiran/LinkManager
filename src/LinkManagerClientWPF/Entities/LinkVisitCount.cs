using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [ForeignKey("LinkId")]
    public Link Link { get; set; }

}
