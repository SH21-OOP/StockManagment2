using Models;
using System.Collections.Generic;
using Web.BL.Abstract;

namespace BL.Abstract
{
    public interface IProductService : IGenericService<ProductDTO>
    {
        List<ProductDTO> GetAll();
        ProductDTO Get(int id);
        void Add(ProductDTO dto);
        void Update(ProductDTO dto);
        void Delete(int id);
    }
}
