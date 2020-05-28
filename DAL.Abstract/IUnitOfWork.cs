using System;

namespace DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        void Dispose(bool disposing);
    }
}
