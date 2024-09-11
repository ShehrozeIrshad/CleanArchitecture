using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        User SaveUser(User user);   
        User GetUserByName(string name);
    }
}
