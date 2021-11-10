using System.Collections.Generic;
using LinkManagerApi.Contracts.V1.Responses;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetAllLinksQuery: IRequest<IEnumerable< LinkResponse >>
{

}