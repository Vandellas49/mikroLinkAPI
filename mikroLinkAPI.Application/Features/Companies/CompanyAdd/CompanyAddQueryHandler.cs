using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.CompanyAdd
{
    internal sealed class CompanyAddQueryHandler(IKafkaProducerService _producerService, ICurrentUserService currentUserService, ICompanyRepository companyRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CompanyAddQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CompanyAddQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await companyRepository.FirstOrDefaultAsync(p => p.Name == request.Name, cancellationToken);
            if (result != null)
                return Result<string>.Failure("Firma zaten kayıtlı");
            var company = mapper.Map<Company>(request);
            await companyRepository.AddAsync(company, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var message = new FirmaEvent { Zaman = DateTime.UtcNow, Text = $"{request.Name} firması {currentUserService.UserName} tarafından oluşturuldu", Type = "info" };
            await _producerService.SendMessageAsync(message,currentUserService.UserCompanyId);
            return "Firma başarıyla kaydedildi";
        }
    }
}
