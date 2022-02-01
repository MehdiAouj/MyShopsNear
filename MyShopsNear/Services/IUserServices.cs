using MyShopsNear.Models;

namespace MyShopsNear.Services
{
    public interface IUserServices
    {
        List<Users> Get();
        Users Get(string username);
        Users Create(Users user);
        void Update(string id, Users user);
        void Remove(string id);
    }
}
