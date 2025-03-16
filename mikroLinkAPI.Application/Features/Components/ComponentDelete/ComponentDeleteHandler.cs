
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentDelete
{
    internal sealed class ComponentDeleteHandler(IComponentRepository componentRepository, IUnitOfWork unitOfWork) : IRequestHandler<ComponentDeleteCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ComponentDeleteCommand request, CancellationToken cancellationToken)
        {
            var component = await componentRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (component == null)
                return Result<string>.Failure("Bu parça bulunamdı");
            componentRepository.Delete(component);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Parça başarıyla silindi";
        }
    }
}
