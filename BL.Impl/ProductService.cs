using BL.Abstract;
using DAL.Impl;
using DAL.Impl.EFCore;
using DAL.Impl.Mappers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace BL.Impl
{
    public class ProductService : IProductService
    {
        readonly ProductMapper Mapper;
        readonly EfCoreProductRepository Repo;

        public ProductService(UnitOfWork unitOfWork)
        {
            Repo = unitOfWork.Products;
            Mapper = new ProductMapper(Repo);
        }

        public List<ProductDTO> GetAll()
        {
            return Repo.GetAll().Result.Select(e => Mapper.Map(e)).ToList();
        }

        public ProductDTO Get(int id)
        {
            return Mapper.Map(Repo.Get(id).Result);
        }

        public void Add(ProductDTO dto)
        {
            Repo.Add(Mapper.DeMap(dto)).Wait();
        }

        public void Update(ProductDTO dto)
        {
            Repo.Update(Mapper.DeMap(dto)).Wait();
        }

        public void Delete(int id)
        {
            Repo.Delete(id).Wait();
        }
    }
}
