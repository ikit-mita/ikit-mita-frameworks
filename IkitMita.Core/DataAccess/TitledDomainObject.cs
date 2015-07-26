using System.Diagnostics;

namespace IkitMita.DataAccess
{
    [DebuggerDisplay("[{Id}] {Title}")]
    public class TitledDomainObject : DomainObject
    {
        public virtual string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
