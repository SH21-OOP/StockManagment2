using Entities;

namespace DAL.Impl.EFCore
{
    public class EfCoreProductRepository : EfCoreRepository<Product, StockManagmentContext>
    {
        public EfCoreProductRepository(StockManagmentContext context) : base(context)
        {

        }
    }
}
