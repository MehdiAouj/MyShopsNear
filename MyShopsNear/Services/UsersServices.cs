using MyShopsNear.Models;
using MongoDB.Driver;

namespace MyShopsNear.Services
{
    public class UsersServices : IUserServices
    {
        private readonly IMongoCollection<Users> _users;

        public UsersServices(IShopsNearDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<Users>(settings.UsersCollectionName);
        }
        public Users Create(Users user)
        {
            _users.InsertOne(user);

            return user;
        }

        public List<Users> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public Users Get(string username)
        {
            return _users.Find(user => user.Username == username).FirstOrDefault();
        }

        public Users Get(UserLogin userLogin)
        {
            Users user = _users.Find(o => o.Username.Equals(userLogin.UserName,
            StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password)).FirstOrDefault();

            return user;
        }

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }

        public void Update(string id, Users user)
        {
            _users.ReplaceOne(user => user.Id == id, user);
        }

    }
}
