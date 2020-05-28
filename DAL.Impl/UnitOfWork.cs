using DAL.Abstract;
using DAL.Impl.EFCore;
using System;

namespace DAL.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockManagmentContext _context;
        private readonly EfCoreProductRepository productRepository;
        private readonly EfCoreComercialProductGroupRepository productGroupRepository;

        public UnitOfWork()
        {
            _context = new StockManagmentContext();
            productRepository = new EfCoreProductRepository(_context);
            productGroupRepository = new EfCoreComercialProductGroupRepository(_context);
        }

        public EfCoreProductRepository Products
        {
            get
            {
                return productRepository;
            }
        }

        public EfCoreComercialProductGroupRepository ProductGroups
        {
            get
            {
                return productGroupRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
