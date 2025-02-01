using System.Configuration;
using System.Text;

namespace ProgrammingDemo
{
    public class EncryptionMode : IMode
    {
        private readonly Dictionary<string, string> _encryptionDict;
        private List<string> _wordToEncryptList;       

        public EncryptionMode()
        {
            _encryptionDict = new Dictionary<string, string>();
            _wordToEncryptList = new List<string>();
            Initialize();
        }

        // Note: It can be done from constructer.
        public void Initialize()
        {
            var strMappingFilePath = ConfigurationManager.AppSettings["MappingFilePath"];
            var encryptionList = File.ReadLines(strMappingFilePath ?? string.Empty).ToList();
            
            foreach (var encryption in encryptionList)
            {
                var chars = encryption.Split(HelperFunctions.delimeter);
                _encryptionDict.Add(chars[0], chars[1]);
            }

            var strWordsToEncryptFilePath = ConfigurationManager.AppSettings["WordsToEncryptFilePath"];
            _wordToEncryptList = File.ReadLines(strWordsToEncryptFilePath ?? string.Empty).ToList();
        }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            List<string> encryptedOutputList = new List<string>();

            foreach (var wordToEncrypt in _wordToEncryptList)
            {
                sb.Clear();
                foreach (var word in wordToEncrypt)
                {
                    string charToReplace;
                    _encryptionDict.TryGetValue(word.ToString(), out charToReplace);
                    if (string.IsNullOrWhiteSpace(charToReplace))
                    {
                        charToReplace = word.ToString();
                    }

                    sb.Append(charToReplace);
                }

                encryptedOutputList.Add($"{wordToEncrypt} - {sb.ToString()}");
            }

            if (encryptedOutputList.Count > 0)
            {
                var strEncryptedFilePath = ConfigurationManager.AppSettings["EncryptedFilePath"];
                File.WriteAllLinesAsync(strEncryptedFilePath ?? string.Empty, encryptedOutputList);
                Console.WriteLine($"Output is written to {strEncryptedFilePath} file.");
            }
        }
    }
}
