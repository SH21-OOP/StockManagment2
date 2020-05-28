using Models;
using System.Collections.Generic;
using Web.BL.Abstract;

namespace BL.Abstract
{
    public interface IComercialProductGroupService : IGenericService<ComercialProductGroupDTO>
    {
        List<ComercialProductGroupDTO> GetAll();
        ComercialProductGroupDTO Get(int id);
        void Add(ComercialProductGroupDTO dto);
        void Update(ComercialProductGroupDTO dto);
        void Delete(int id);
    }
}
