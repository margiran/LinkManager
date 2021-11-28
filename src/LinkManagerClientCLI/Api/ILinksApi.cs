using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerClientCLI.Api.ApiResponse;
using Refit;

namespace LinkManagerClientCLI.Api;

public interface ILinksApi
{
    [Get("/api/links?updateAfter={updateAt}")]
    Task<IReadOnlyCollection< LinkSearchResponse>> SearchUpdateLinks(string? updateAt);
}