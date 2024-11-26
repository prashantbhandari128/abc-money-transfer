using ABCMoneyTransfer.DTO;
using ABCMoneyTransfer.Persistence.Entities;
using AutoMapper;

namespace ABCMoneyTransfer.DomainProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define object-to-object mapping here
            CreateMap<TransferDto, Transaction>();
        }
    }
}
