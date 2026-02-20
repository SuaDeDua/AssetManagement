# Project File Map

## src
### AssetManagement.Application
- AssetManagement.Application.csproj
- Class1.cs

### AssetManagement.Domain
- AssetManagement.Domain.csproj
- **Assets/**
  - Asset.cs
  - AssetErrors.cs
  - AssetType.cs
  - Prefix.cs
  - SerialNumber.cs
- **Shared/**
  - Description.cs
  - Name.cs
- **Tickets/** (Empty/Placeholder)
- **Schema/** (Empty/Placeholder)

### AssetManagement.SharedKernel
- AssetManagement.SharedKernel.csproj
- AuditableEntity.cs
- Entity.cs (Base Entity class)
- Ensure.cs
- Error.cs
- ErrorType.cs
- IAggregateRoot.cs
- IDomainEvent.cs
- IRepository.cs
- Result.cs
- ValidationError.cs

## docs
- filemap.md