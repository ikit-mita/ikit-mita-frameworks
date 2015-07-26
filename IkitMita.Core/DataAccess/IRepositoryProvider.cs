using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace IkitMita.DataAccess
{
    public interface IRepositoryProvider : IDisposable
    {
        [NotNull]
        IRepository<T> GetRepository<T>() where T : class, IDomainObject;

        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void RevertChanges();
        Task RevertChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
