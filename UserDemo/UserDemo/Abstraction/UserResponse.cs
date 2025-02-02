
namespace UserDemo.Abstraction
{
    public class UserResponse
    {
        public int AccountNumber { get; set; }
        public required string UserName { get; set; }
        public int DynamicsCredits { get; set; }
        public string? Token { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
