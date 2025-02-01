using UserDemo.Abstraction;

namespace UserDemo.Repository
{
    public interface IUserManagementRepository
    {
        Task<int> RegisterUser(UserRequest user);
        Task<int> AddDynamicsCredit(int AccountNumber);
        Task<User> GetUserByUsername(string userName, string password);
        Task<int> UpdateIsLoggedIn(int AccountNumber);
        Task<int> LogoutUser(int AccountNumber);
        Task<UserResponse> GetUserByAccountNumber(int AccountNumber);

    }
}
