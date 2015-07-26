using System.Diagnostics;

namespace IkitMita.DataAccess
{
    [DebuggerDisplay("[{Id}] {Name}")]
    public class NamedDomainObject : DomainObject
    {
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
