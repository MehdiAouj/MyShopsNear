namespace MyShopsNear.Models
{
    public interface IShopsNearDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        
    }
}
