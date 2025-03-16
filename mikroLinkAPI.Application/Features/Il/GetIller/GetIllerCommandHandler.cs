using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;


namespace mikroLinkAPI.Application.Features.Il.GetIller
{
    internal sealed class GetIllerCommandHandler(IILRepository repository,IMapper mapper) : IRequestHandler<GetIllerCommand, Result<List<IlVM>>>
    {
        public async Task<Result<List<IlVM>>> Handle(GetIllerCommand request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<IlVM>>(await repository.GetAll().ToListAsync(cancellationToken));
        }
    }

}
