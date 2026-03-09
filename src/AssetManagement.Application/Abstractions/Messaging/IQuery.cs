using AssetManagement.Domain.Shared.Common;
using MediatR;

namespace AssetManagement.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
