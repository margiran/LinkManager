using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerApi.Domain
{
    public class Link
    {
        public Link()
        {
            UpdateAt= DateTimeOffset.UtcNow;
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Argument { get; set; }

        [MaxLength(500)]
        [Required]
        public string FileName { get; set; }

        public bool InternetNeeded { get; set; }

        [MaxLength(500)]
        [Required]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }
 
        [MaxLength(50)]
        public string DefaultPassword { get; set; }

        public bool IsDelete { get; set; }

        public int VisitedCount { get; set; }

        public DateTimeOffset UpdateAt { get;  set; } 

        [Required]
        public int Order { get; set; }

    }
}