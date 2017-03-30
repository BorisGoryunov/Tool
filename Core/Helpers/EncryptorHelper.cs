namespace Core.Helpers
{
    public static class EncryptorHelper
    {
        private static string PassKey
        {
            get
            {
                return "2BFA004AD2EC";
            }
        }

        public static string Encrypt(string value)
        {
            string key = Core.Common.Encryptor.GenerateAPassKey(PassKey);
            return Core.Common.Encryptor.Encrypt(value, key);
        }

        public static string Decrypt(string value)
        {
            string key = Core.Common.Encryptor.GenerateAPassKey(PassKey);
            return Core.Common.Encryptor.Decrypt(value, key);
        }

    }
}
