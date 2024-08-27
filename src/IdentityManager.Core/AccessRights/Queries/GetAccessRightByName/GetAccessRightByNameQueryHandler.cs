using IdentityManager.Data.Repositories;
using MediatR;

namespace IdentityManager.Core.AccessRights.Queries.GetAccessRightByName
{
    internal class GetAccessRightByNameQueryHandler : IRequestHandler<GetAccessRightByNameQuery, GetAccessRightQueryResult?>
    {
        private readonly IAccessRightRepository _repository;

        public GetAccessRightByNameQueryHandler(IAccessRightRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GetAccessRightQueryResult?> Handle(GetAccessRightByNameQuery query, CancellationToken cancellationToken)
        {
            var accessRight = await _repository.GetByNameAsync(query.Name, cancellationToken);

            return accessRight is null ? null : accessRight.Map();
        }
    }
}
