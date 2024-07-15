using PanteonWebAPI.Models.Entities;

namespace PanteonWebAPI.Interfaces
{
    public interface IUser
    {
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

        Task<User> GetUserByIdAsync(int userId);

        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
