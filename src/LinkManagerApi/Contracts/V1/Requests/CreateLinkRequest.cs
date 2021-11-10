using System;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerApi.Contracts.V1.Requests;

public class CreateLinkRequest
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Argument { get; set; }

    [MaxLength(500)]
    [Required]
    public string FileName { get; set; }

    public bool InternetNeeded { get; set; } = false;

    [MaxLength(500)]
    [Required]
    public string ShortDescription { get; set; }

    public string Description { get; set; }

    [MaxLength(50)]
    public string UserName { get; set; }

    [MaxLength(50)]
    public string DefaultPassword { get; set; }

    public int VisitedCount { get; set; }

    [Required]
    public int Order { get; set; }

}
