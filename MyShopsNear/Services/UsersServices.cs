using MyShopsNear.Models;
using MongoDB.Driver;

namespace MyShopsNear.Services
{
    public class UsersServices : IUserServices
    {
        private readonly IMongoCollection<Users> _users;

        public static Users usr = new Users();

        private readonly IMongoCollection<Shops> _shops;

        public UsersServices(IShopsNearDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<Users>(settings.UsersCollectionName);
            _shops = database.GetCollection<Shops>(settings.UsersCollectionName);
        }
        //Done
        public Users Create(Users user)
        {
            var eml = _users.Find(em => em.Email == user.Email).FirstOrDefault();

            if (eml == null)
            {
                _users.InsertOne(user);
                return user;
            }
            else
            {
                throw new Exception("Email Already Exists! Try another One :)");
            }
        }

        public List<Users> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public Users Get(string username)
        {
            return _users.Find(user => user.Username == username).FirstOrDefault();
        }

        

        public void Remove(string username)
        {
            _users.DeleteOne(user => user.Username == username);
        }

        public void Update(string id, Users user)
        {
            _users.ReplaceOne(user => user.Id == id, user);
        }

    }
}
