using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    public class LinkModel
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

        public int Order { get; set; }

        public int LinkVisitCount { get; set; }
    }
}
