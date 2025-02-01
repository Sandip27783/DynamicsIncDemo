using Dapper;
using UserDemo.Abstraction;
using UserDemo.Helper;
using UserDemo.Service;

namespace UserDemo.Repository
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly ILogger<UserManagementRepository> _logger;
        private readonly IDapperContext _context;
        private readonly EncryptionService _encryptionService;
        private readonly string spName = "SP_UserAccount";

        public UserManagementRepository(ILogger<UserManagementRepository> logger,
            IDapperContext context, EncryptionService encryptionService
            )
        {
            _logger = logger;
            _context = context;
            _encryptionService = encryptionService;

        }
        public async Task<int> RegisterUser(UserRequest user)
        {
            _logger.LogInformation("Inside UserManagementRepository: RegisterUser");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "RegisterUser");
                parameters.Add("UserName", user.UserName);
                var encryptedPassword = _encryptionService.EncryptData(user.Password);
                parameters.Add("Password", encryptedPassword);
                parameters.Add("AccountNumber", 1);
                parameters.Add("DynamicsCredits", 1);

                var returnVal = await connection.ExecuteScalarAsync(spName, parameters);

                return Convert.ToInt32(returnVal);
            }
        }

        public async Task<int> AddDynamicsCredit(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementRepository: AddDynamicsCredit");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "AddDynamicsCredit");
                parameters.Add("AccountNumber", AccountNumber);

                var returnVal = await connection.ExecuteScalarAsync(spName, parameters);

                return Convert.ToInt32(returnVal);
            }
        }

        public async Task<UserResponse> GetUserByAccountNumber(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementRepository: GetUserById");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "GetUserById");
                parameters.Add("AccountNumber", AccountNumber);

                var returnVal = await connection.QuerySingleOrDefaultAsync<UserResponse>(spName, parameters);

                if (returnVal != null)
                    return returnVal;
                else
                    return new UserResponse { AccountNumber = 0, UserName = string.Empty, DynamicsCredits = 0, IsLoggedIn = false };
            }
        }

        public async Task<User> GetUserByUsername(string userName, string password)
        {
            _logger.LogInformation("Inside UserManagementRepository: GetUserByUsername");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "GetUserByUsername");
                parameters.Add("UserName", userName);

                var returnVal = await connection.QuerySingleOrDefaultAsync<User>(spName, parameters);

                if (returnVal != null)
                    return returnVal;
                else
                    return new User { AccountNumber = 0, UserName = string.Empty, Password = string.Empty };
            }
        }

        public async Task<int> UpdateIsLoggedIn(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementRepository: UpdateIsLoggedIn");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "UpdateIsLoggedIn");
                parameters.Add("AccountNumber", AccountNumber);

                var returnVal = await connection.ExecuteScalarAsync(spName, parameters);

                return Convert.ToInt32(returnVal);
            }
        }

        public async Task<int> LogoutUser(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementRepository: LogoutUser");
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Mode", "LogoutUser");
                parameters.Add("AccountNumber", AccountNumber);

                var returnVal = await connection.ExecuteScalarAsync(spName, parameters);

                return Convert.ToInt32(returnVal);
            }
        }
    }
}
