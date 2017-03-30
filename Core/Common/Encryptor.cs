using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Core.Common
{

    public class Encryptor
    {
        public static string GenerateAPassKey(string passphrase)
        {
            // Pass Phrase can be any string
            string passPhrase = passphrase;
            // Salt Value can be any string(for simplicity use the same value as used for the pass phrase)
            string saltValue = passphrase;
            // Hash Algorithm can be "SHA1 or MD5"
            const string hashAlgorithm = "SHA1";
            // Password Iterations can be any number
            const int passwordIterations = 2;
            // Key Size can be 128,192 or 256
            const int keySize = 256;
            // Convert Salt passphrase string to a Byte Array
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            // Using System.Security.Cryptography.PasswordDeriveBytes to create the Key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            //When creating a Key Byte array from the base64 string the Key must have 32 dimensions.
            byte[] Key = pdb.GetBytes(keySize / 11);
            String KeyString = Convert.ToBase64String(Key);

            return KeyString;
        }
        //Save the keystring some place like your database and use it to decrypt and encrypt
        //any text string or text file etc. Make sure you dont lose it though.


        public static string Encrypt(string plainStr, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = 256;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.ISO10126;
            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);
            aesEncryption.Key = KeyInBytes;
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(plainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        public static string Decrypt(string encryptedText, string KeyString)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = 256;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.ISO10126;
            byte[] KeyInBytes = Encoding.UTF8.GetBytes(KeyString);
            aesEncryption.Key = KeyInBytes;
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64CharArray(encryptedText.ToCharArray(), 0, encryptedText.Length);
            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        }

    }

    //Use:
    //String KeyString = GenerateAPassKey("PassKey");
    //String EncryptedPassword = Encrypt("25Characterlengthpassword!", KeyString);
    //String DecryptedPassword = Decrypt(EncryptedPassword, KeyString);
    class SimplerAES
    {
        private static byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
        private ICryptoTransform encryptor, decryptor;
        private UTF8Encoding encoder;

        public static string EncryptString(string value)
        {
            return new SimplerAES().Encrypt(value);
        }
        public static string DecryptString(string value)
        {
            return new SimplerAES().Decrypt(value);
        }
        public SimplerAES()
        {
            RijndaelManaged rm = new RijndaelManaged();
            encryptor = rm.CreateEncryptor(key, vector);
            decryptor = rm.CreateDecryptor(key, vector);
            encoder = new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }
    class PasswordEncoder
    {
        string mEncryptedPassword;
        // Change the two values below to be something other than the example.
        // Once changed and in use, do not change the value below again or you
        // won't be able to decrypt previously stored passwords.
        string mByteArray = "%$#>#%232s+as#l)URa0$!@";
        byte[] mInitializationVector = { 0x01, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xf7, 0xEF };

        public PasswordEncoder()
        {

        }

        public PasswordEncoder(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, mByteArray);
        }

        public string EncryptWithByteArray(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, mByteArray);
            return mEncryptedPassword;
        }

        private string EncryptWithByteArray(string inPassword, string inByteArray)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(inByteArray.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(inPassword);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputArray, 0, inputArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DecryptWithByteArray()
        {
            return DecryptWithByteArray(mEncryptedPassword, mByteArray);
        }

        private string DecryptWithByteArray(string strText, string strEncrypt)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptedPassword
        {
            get { return mEncryptedPassword; }
            set { mEncryptedPassword = value; }
        }

        public string ByteArray
        {
            get { return mByteArray; }
            set { mByteArray = value; }
        }
    }




}
