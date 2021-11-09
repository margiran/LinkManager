using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinkManagerApi.Domain
{
    public class Link
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [MaxLength(500)]
        [Required]
        public string Explorer { get; set; }

        public bool Internet { get; set; }

        [MaxLength(500)]
        [Required]

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        [MaxLength(50)]

        public string UserName { get; set; }
        [MaxLength(50)]

        public string DefaultPassword { get; set; }

        public bool IsDelete { get; set; }

        public int Visited { get; set; }

        public DateTime UpDateAt { get; set; }
        [Required]

        public int Order { get; set; }

    }
}