using Assetly.Shared.Domain.Common;
using MediatR;

namespace Assetly.Modules.Assets.Application.Common.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
