using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Core.AccessRights.Queries.GetAccessRightByName
{
    public sealed record GetAccessRightByNameQuery(string Name) : IRequest<GetAccessRightQueryResult?>;
}
