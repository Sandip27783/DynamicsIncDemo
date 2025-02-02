using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDemo.Abstraction;
using UserDemo.Service;

namespace UserDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(ILogger<UserManagementController> logger,
            IUserManagementService userManagementService)
        {
            _logger = logger;
            _userManagementService = userManagementService;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser(UserRequest user)
        {
            _logger.LogInformation("Inside UserManagementController: RegisterUser.");

            StringBuilder sb = new StringBuilder();
            if (user == null)
            {
                return await Task.FromResult(BadRequest("Incorrect data."));
            }
            if (String.IsNullOrWhiteSpace(user.UserName))
            {
                sb.AppendLine("Please insert username.");
            }
            if (String.IsNullOrWhiteSpace(user.Password))
            {
                sb.AppendLine("Please insert password.");
            }
            else if (user.Password.Length < 8 || user.Password.Length > 16)
            {
                sb.AppendLine("Password length must be 8 to 16 characters.");
            }
            if (sb.Length > 0)
            {
                return await Task.FromResult(BadRequest(sb.ToString()));
            }

            return await Task.FromResult(Ok(await _userManagementService.RegisterUser(user)));
        }
        
        [HttpPost("AddDynamicsCredit"), Authorize]
        public async Task<ActionResult> AddDynamicsCredit(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementController: AddDynamicsCredit");

            return await Task.FromResult(Ok(await _userManagementService.AddDynamicsCredit(AccountNumber)));
        }

        [HttpPost("ValidateUser")]
        public async Task<ActionResult> ValidateUser(UserRequest user)
        {
            _logger.LogInformation("Inside UserManagementController: ValidateUser.");

            return await Task.FromResult(Ok(await _userManagementService.ValidateUser(user)));
        }

        [HttpPost("UpdateIsLoggedIn")]
        public async Task<ActionResult> UpdateIsLoggedIn(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementController: UpdateIsLoggedIn.");

            return await Task.FromResult(Ok(await _userManagementService.UpdateIsLoggedIn(AccountNumber)));
        }

        [HttpPost("LogoutUser"), Authorize]
        public async Task<ActionResult> LogoutUser(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementController: LogoutUser.");

            return await Task.FromResult(Ok(await _userManagementService.LogoutUser(AccountNumber)));
        }

        [HttpPost("GetUserByAccountNumber"), Authorize]
        public async Task<ActionResult> GetUserByAccountNumber(int AccountNumber)
        {
            _logger.LogInformation("Inside UserManagementController: GetUserById.");

            return await Task.FromResult(Ok(await _userManagementService.GetUserByAccountNumber(AccountNumber)));
        }
    }
}
