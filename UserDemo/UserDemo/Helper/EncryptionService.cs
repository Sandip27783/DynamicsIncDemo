using Microsoft.AspNetCore.DataProtection;

namespace UserDemo.Helper
{
    public class EncryptionService
    {
        private readonly IDataProtector _protector;

        // Constructor to initialize the IDataProtector using dependency injection
        public EncryptionService(IDataProtectionProvider provider, IConfiguration configuration)
        {
            var purpose = configuration.GetSection("AppSettings:ProtectorPurpose").Value;
            _protector = provider.CreateProtector(purpose ?? "MyProtectorPurpose");            
        }

        // Method to encrypt plain text data
        public string EncryptData(string plainText)
        {
            return _protector.Protect(plainText);
        }

        // Method to decrypt the encrypted data
        public string DecryptData(string encryptedData)
        {
            try
            {
                return _protector.Unprotect(encryptedData);
            }
            catch (Exception ex)
            {
                // If decryption fails (e.g., data is tampered or invalid), handle the exception
                return $"Decryption failed: {ex.Message}";
            }
        }
    }
}
