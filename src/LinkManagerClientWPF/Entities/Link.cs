using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerClientWPF.Entities;

public class Link
{
    public Link()
    {
    }
    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Argument { get; set; }

    public string FileName { get; set; }

    public bool InternetNeeded { get; set; }

    public string ShortDescription { get; set; }

    public string Description { get; set; }

    public string UserName { get; set; }

    public string DefaultPassword { get; set; }

    public bool IsDelete { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public int Order { get; set; }

    public LinkVisitCount LinksVisitCount { get; set; }
}
