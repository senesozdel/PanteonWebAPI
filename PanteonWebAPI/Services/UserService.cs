using Microsoft.EntityFrameworkCore;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Data;
using PanteonWebAPI.Models.Entities;
using PanteonWebAPI.Models.ResponseModels;

namespace PanteonWebAPI.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {

            _db = db;
        }

        public async Task<AddedUserResponse> AddUserAsync(User user)
        {
            try
            {
                var userResponse = new AddedUserResponse();

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null");
                }

               
                bool isUserNameTaken = await _db.Users.AnyAsync(u => u.UserName == user.UserName);
                bool isEmailTaken = await _db.Users.AnyAsync(u => u.Email == user.Email);

                if (isEmailTaken)
                {
                    userResponse.Message = "Email is already registered. Please use another email.";
                   
                    return userResponse;
                }
                if (isUserNameTaken)
                {
                    userResponse.Message = "Username is already taken. Please choose another username.";

                    return userResponse;
                }



                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                userResponse.UserEmail = user.Email;
                userResponse.UserName = user.UserName;

                return userResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenemedi: " + ex.Message);
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            try
            {
              
                var user = await _db.Users.FindAsync(userId);

                if (user != null)
                {
                    user.IsDeleted = true;
                    _db.Users.Update(user);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı silinemedi: " + ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _db.Users.Where(u => !u.IsDeleted).ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcılar getirilemedi: " + ex.Message);
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

                if (user == null)
                {
                    throw new KeyNotFoundException("Kullanıcı bulunamadı.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı getirilemedi: " + ex.Message);
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
               
                var existingUser = await _db.Users.FindAsync(user.Id);

                if (existingUser == null || existingUser.IsDeleted)
                {
                    throw new KeyNotFoundException("Kullanıcı bulunamadı.");
                }

                
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;

                _db.Users.Update(existingUser);
                await _db.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı güncellenemedi: " + ex.Message);
            }
        }

    }
}
