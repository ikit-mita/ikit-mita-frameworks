using System.Linq;

namespace IkitMita.DataAccess
{
    public interface IRepository<TItem> where TItem : class, IDomainObject 
    {
        void Add(TItem item);
        void Remove(TItem item);
        TItem Find(int id);
        IQueryable<TItem> GetAll();
    }
}
