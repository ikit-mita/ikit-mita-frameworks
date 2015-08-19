using System.Collections.ObjectModel;

namespace Example.ViewModels
{
    public static class MessageButtons
    {
        public static readonly ReadOnlyCollection<string> Ok = new ReadOnlyCollection<string>(new[] { "OK" });
        public static readonly ReadOnlyCollection<string> OkCancel = new ReadOnlyCollection<string>(new[] { "OK", "Cancel" });
        public static readonly ReadOnlyCollection<string> YesNo = new ReadOnlyCollection<string>(new[] { "Yes", "No" });
    }
}
