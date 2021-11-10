using System;

namespace LinkManagerApi.Dtos;

public class LinkResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Argument { get; set; }

    public string FileName { get; set; }

    public bool InternetNeeded { get; set; }

    public string ShortDescription { get; set; }

    public string Description { get; set; }

    public string UserName { get; set; }

    public string DefaultPassword { get; set; }

    public int VisitedCount { get; set; }

    public DateTimeOffset UpdateAt { get; set; }

    public int Order { get; set; }

}
