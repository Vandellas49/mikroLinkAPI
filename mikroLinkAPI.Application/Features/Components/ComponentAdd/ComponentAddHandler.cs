
using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentAdd
{
    internal sealed class ComponentAddHandler(IComponentRepository componentRepository,IMapper mapper,IUnitOfWork unitOfWork) : IRequestHandler<ComponentAddCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ComponentAddCommand request, CancellationToken cancellationToken)
        {
            if (componentRepository.Any(p => p.Id == request.Id))
               return Result<string>.Failure("Bu parça zaten kayıtlı");
            await componentRepository.AddAsync(mapper.Map<Component>(request), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Parça başarıyla eklendi";
        }
    }
}
