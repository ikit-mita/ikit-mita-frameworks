using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using JetBrains.Annotations;

namespace IkitMita.DataAccess
{
    [DebuggerDisplay("[{Id}]{FullName}")]
    public class FullNamedDomainObject : DomainObject
    {
        [Required]
        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        public virtual string LastName { get; set; }

        [NotNull]
        public virtual string Initials
        {
            get
            {
                string initials = string.Empty;

                if (!FirstName.IsNullOrEmpty())
                {
                    initials = FirstName[0] + ".";

                    if (!MiddleName.IsNullOrEmpty())
                    {
                        initials += " " + MiddleName[0] + ".";
                    }
                }

                return initials;
            }
        }

        [NotNull]
        public virtual string FullName
        {
            get { return string.Join(" ", LastName, FirstName, MiddleName); }
        }

        [NotNull]
        public virtual string ShortName
        {
            get { return string.Join(" ", LastName, Initials); }
        }
    }
}
