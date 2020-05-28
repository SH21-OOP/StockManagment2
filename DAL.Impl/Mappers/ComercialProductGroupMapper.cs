using DAL.Abstract;
using DAL.Impl.EFCore;
using Entities;
using Models;

namespace DAL.Impl.Mappers
{
    public class ComercialProductGroupMapper : IMapper<ComercialProductGroup, ComercialProductGroupDTO, EfCoreComercialProductGroupRepository>
    {
        public EfCoreComercialProductGroupRepository repo;

        public ComercialProductGroupMapper(EfCoreComercialProductGroupRepository repo)
        {
            this.repo = repo;
        }

        public ComercialProductGroupDTO Map(ComercialProductGroup entity)
        {
            return new ComercialProductGroupDTO()
            {
                Id = entity.Id,
                DeliveryTime = entity.DeliveryTime,
                Ends = entity.Ends,
                Name = entity.Name,
                PurchasePrice = entity.PurchasePrice,
                SellPrice = entity.SellPrice,
                TermOfUse = entity.TermOfUse
            };
        }

        public ComercialProductGroup DeMap(ComercialProductGroupDTO dto)
        {
            ComercialProductGroup entity = repo.Get(dto.Id).Result;
            if (entity == null)
                return new ComercialProductGroup()
                {
                    Id = dto.Id,
                    TermOfUse = dto.TermOfUse,
                    SellPrice = dto.SellPrice,
                    PurchasePrice = dto.PurchasePrice,
                    Name = dto.Name,
                    DeliveryTime = dto.DeliveryTime,
                    Ends = dto.Ends,
                };
            entity.Id = dto.Id;
            entity.TermOfUse = dto.TermOfUse;
            entity.SellPrice = dto.SellPrice;
            entity.PurchasePrice = dto.PurchasePrice;
            entity.Name = dto.Name;
            entity.DeliveryTime = dto.DeliveryTime;
            entity.Ends = dto.Ends;
            return entity;
        }
    }
}
