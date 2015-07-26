using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace IkitMita.DataAccess
{
    public interface IRepository<TItem> where TItem : class, IDomainObject 
    {
        void Add([NotNull]TItem item);
        
        void Remove([NotNull]TItem item);
        
        TItem Find(int id);
        
        Task<TItem> FindAsync(int id);

        [NotNull]
        IQueryable<TItem> GetAll();
    }
}
