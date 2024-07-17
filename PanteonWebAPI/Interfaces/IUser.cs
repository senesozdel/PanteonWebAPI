using PanteonWebAPI.Models.Entities;
using PanteonWebAPI.Models.ResponseModels;

namespace PanteonWebAPI.Interfaces
{
    public interface IUser
    {
        Task<AddedUserResponse> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

        Task<User> GetUserByIdAsync(int userId);

        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
