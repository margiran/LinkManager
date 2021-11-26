using LinkManagerClientWPF.Api.ApiResponse;
using LinkManagerClientWPF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Common
{
    public static class CustomMapper
    {
        public static Link ToLink(this LinkSearchResponse apiLinkResponse)
        {
            return new Link
            {
                Argument = apiLinkResponse.Argument,
                DefaultPassword = apiLinkResponse.DefaultPassword,
                Description = apiLinkResponse.Description,
                FileName = apiLinkResponse.FileName,
                Id = apiLinkResponse.Id,
                InternetNeeded = apiLinkResponse.InternetNeeded,
                IsDelete = apiLinkResponse.IsDelete,
                Order = apiLinkResponse.Order,
                ShortDescription = apiLinkResponse.ShortDescription,
                Title = apiLinkResponse.Title,
                UpdateAt = apiLinkResponse.UpdateAt,
                UserName = apiLinkResponse.UserName
            };
        }
    }
}
