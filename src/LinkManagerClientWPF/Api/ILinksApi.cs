using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerClientWPF.Api.ApiResponse;
using LinkManagerClientWPF.Entities;
using Refit;

namespace LinkManagerClientWPF.Api;

public interface ILinksApi
{
    [Get("/api/links?updateAfter={updateAt}")]
    Task<IReadOnlyCollection<LinkSearchResponse>> SearchUpdateLinks(string? updateAt);
}