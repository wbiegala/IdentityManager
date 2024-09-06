using IdentityManager.Data.Repositories;
using MediatR;

namespace IdentityManager.Core.AccessRights.Queries.GetAccessRightByCode
{
    internal class GetAccessRightByCodeQueryHandler : IRequestHandler<GetAccessRightByCodeQuery, GetAccessRightQueryResult?>
    {
        private readonly IAccessRightRepository _repository;

        public GetAccessRightByCodeQueryHandler(IAccessRightRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GetAccessRightQueryResult?> Handle(GetAccessRightByCodeQuery query, CancellationToken cancellationToken)
        {
            var accessRight = await _repository.GetByCodeAsync(query.Code, cancellationToken: cancellationToken);

            return accessRight is null ? null : accessRight.Map();
        }
    }
}
