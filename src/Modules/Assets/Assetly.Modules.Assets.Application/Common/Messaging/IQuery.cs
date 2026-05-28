using Assetly.Shared.Domain.Common;
using MediatR;

namespace Assetly.Modules.Assets.Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
