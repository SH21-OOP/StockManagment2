using DAL.Abstract;
using DAL.Impl.EFCore;
using Entities;
using Models;

namespace DAL.Impl.Mappers
{
    public class ProductMapper : IMapper<Product, ProductDTO, EfCoreProductRepository>
    {
        public EfCoreProductRepository repo;

        public ProductMapper(EfCoreProductRepository repo)
        {
            this.repo = repo;
        }

        public ProductDTO Map(Product entity)
        {
            return new ProductDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                ComercialProductGroupID = entity.ComercialProductGroupID,
                Expiration = entity.Expiration,
                Preparation = entity.Preparation,
                Quantity = entity.Quantity
            };
        }

        public Product DeMap(ProductDTO dto)
        {
            Product entity = repo.Get(dto.Id).Result;
            if (entity == null)
                return new Product()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Quantity = dto.Quantity,
                    Preparation = dto.Preparation,
                    Expiration = dto.Expiration,
                    ComercialProductGroupID = dto.ComercialProductGroupID,
                };
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.Quantity = dto.Quantity;
            entity.Preparation = dto.Preparation;
            entity.Expiration = dto.Expiration;
            entity.ComercialProductGroupID = dto.ComercialProductGroupID;
            return entity;
        }
    }
}
