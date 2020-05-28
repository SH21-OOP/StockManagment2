using BL.Abstract;
using DAL.Impl;
using DAL.Impl.EFCore;
using DAL.Impl.Mappers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace BL.Impl
{
    public class ComercialProductGroupService : IComercialProductGroupService
    {
        readonly ComercialProductGroupMapper Mapper;
        readonly EfCoreComercialProductGroupRepository Repo;

        public ComercialProductGroupService(UnitOfWork unitOfWork)
        {
            Repo = unitOfWork.ProductGroups;
            Mapper = new ComercialProductGroupMapper(Repo);
        }

        public List<ComercialProductGroupDTO> GetAll()
        {
            return Repo.GetAll().Result.Select(e => Mapper.Map(e)).ToList();
        }

        public ComercialProductGroupDTO Get(int id)
        {
            return Mapper.Map(Repo.Get(id).Result);
        }

        public void Add(ComercialProductGroupDTO dto)
        {
            Repo.Add(Mapper.DeMap(dto)).Wait();
        }

        public void Update(ComercialProductGroupDTO dto)
        {
            Repo.Update(Mapper.DeMap(dto)).Wait();
        }

        public void Delete(int id)
        {
            Repo.Delete(id).Wait();
        }
    }
}
