
using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentUpdate
{
    internal sealed class ComponentUpdateHandler(IComponentRepository componentRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<ComponentUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ComponentUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!componentRepository.Any(p => p.Id == request.Id))
                return Result<string>.Failure("malzeme bulunamadı");
            var component = mapper.Map<Component>(request);
            componentRepository.Update(component);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Parça başarıyla güncellendi";
        }
    }
}
