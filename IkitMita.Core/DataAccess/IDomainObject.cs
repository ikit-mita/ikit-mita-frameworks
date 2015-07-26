namespace IkitMita.DataAccess
{
    public interface IDomainObject
    {
        int Id { get; set; }
        bool IsNew { get; }
    }
}
