
namespace UserDemo.Abstraction
{
    public class UserRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
    public class ValidateUserResponse
    {
        public int AccountNumber { get; set; }
        public string? Token { get; set; }
    }
    public class UserResponse
    {
        public int AccountNumber { get; set; }
        public required string UserName { get; set; }
        public int DynamicsCredits { get; set; }
        public string? Token { get; set; }
        public bool IsLoggedIn { get; set; }
    }
    public class User
    {
        public int AccountNumber { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public int DynamicsCredits { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime LoingDateTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
