using Assetly.Shared.Domain.Common;
using MediatR;

namespace Assetly.Modules.Assets.Application.Common.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
