using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.ViewModels
{
    public static class MessageButtons
    {
        public static readonly IEnumerable<string> Ok = new ReadOnlyCollection<string>(new[] { "OK" });
        public static readonly IEnumerable<string> OkCancel = new ReadOnlyCollection<string>(new[] { "OK", "Cancel" });
    }
}
