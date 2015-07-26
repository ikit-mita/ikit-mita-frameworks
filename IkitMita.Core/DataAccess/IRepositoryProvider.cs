using System;

namespace IkitMita.DataAccess
{
    public interface IRepositoryProvider : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IDomainObject;
        void SaveChanges();
        void RevertChanges();
    }
}
