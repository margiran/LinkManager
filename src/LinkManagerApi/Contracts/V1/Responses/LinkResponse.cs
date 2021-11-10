using System;

namespace LinkManagerApi.Contracts.V1.Responses
{
    public class LinkResponse
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

        public DateTimeOffset UpDateAt { get; set; }

        public int Order { get; set; }
      
    }
}