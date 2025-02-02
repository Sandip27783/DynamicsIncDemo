using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserDemo.Abstraction;
using UserDemo.Helper;
using UserDemo.Repository;

namespace UserDemo.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ILogger<UserManagementService> _logger;
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly EncryptionService _encryptionService;
        private readonly IConfiguration _configuration;

        public UserManagementService(ILogger<UserManagementService> logger,
            IUserManagementRepository userManagementRepository,
            EncryptionService encryptionService,
            IConfiguration configuration)
        {
            _logger = logger;
            _userManagementRepository = userManagementRepository;
            _encryptionService = encryptionService;
            _configuration = configuration; 
        }

        private async Task<string> GetToken()
        {
            var key = _configuration.GetSection("AppSettings:JWTSecretKey").Value ?? "my_secret_key_dynamics_inc_user_token";
            var issuer = _configuration.GetSection("AppSettings:JWTIssuer").Value ?? "http://dynamicsinc.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Note: Set dynamic values for claim.
            var permClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("valid", "1"),
                new Claim("userid", "1"),
                new Claim("name", "sandip")
            };

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(jwt_token);
        }

        public async Task<int> RegisterUser(UserRequest user)
        {
            _logger.LogInformation("Inside UserManagementService: RegisterUser");
            return await _userManagementRepository.RegisterUser(user);
        }

        public async Task<int> AddDynamicsCredit(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementService: AddDynamicsCredit");
            return await _userManagementRepository.AddDynamicsCredit(AccountNumber);
        }

        public async Task<UserResponse> GetUserByAccountNumber(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementService: GetUserById");
            return await _userManagementRepository.GetUserByAccountNumber(AccountNumber);
        }

        public async Task<ValidateUserResponse> ValidateUser(UserRequest user)
        {
            _logger.LogInformation("Inside UserManagementService: GetUserByUsername");

            var retVal = await _userManagementRepository.GetUserByUsername(user.UserName, user.Password);
            ValidateUserResponse response = new ValidateUserResponse();
            if (retVal.AccountNumber > 0)
            {
                retVal.Password = _encryptionService.DecryptData(retVal.Password);

                if (String.Equals(retVal.Password, user.Password))
                {
                    await UpdateIsLoggedIn(retVal.AccountNumber);
                    var token = await GetToken();
                    response.AccountNumber = retVal.AccountNumber;
                    response.Token = token;
                }
            }

            return response;
        }

        public async Task<int> UpdateIsLoggedIn(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementService: UpdateIsLoggedIn");
            return await _userManagementRepository.UpdateIsLoggedIn(AccountNumber);
        }

        public async Task<int> LogoutUser(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementService: LogoutUser");
            return await _userManagementRepository.LogoutUser(AccountNumber);
        }
    }
}
