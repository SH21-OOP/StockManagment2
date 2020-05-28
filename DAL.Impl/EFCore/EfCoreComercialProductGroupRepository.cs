using Entities;

namespace DAL.Impl.EFCore
{
    public class EfCoreComercialProductGroupRepository : EfCoreRepository<ComercialProductGroup, StockManagmentContext>
    {
        public EfCoreComercialProductGroupRepository(StockManagmentContext context) : base(context)
        {

        }
    }
}
