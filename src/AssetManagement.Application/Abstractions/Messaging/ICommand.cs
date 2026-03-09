using AssetManagement.Domain.Shared.Common;
using MediatR;

namespace AssetManagement.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand { }

public interface IBaseCommand { }
