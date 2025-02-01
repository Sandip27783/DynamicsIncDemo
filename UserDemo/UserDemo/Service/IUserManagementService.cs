using UserDemo.Abstraction;

namespace UserDemo.Service
{
    public interface IUserManagementService
    {
        Task<int> RegisterUser(UserRequest user);
        Task<int> AddDynamicsCredit(int AccountNumber);
        Task<ValidateUserResponse> ValidateUser(UserRequest user);
        Task<int> UpdateIsLoggedIn(int AccountNumber);
        Task<int> LogoutUser(int AccountNumber);
        Task<UserResponse> GetUserByAccountNumber(int AccountNumber);
    }
}
