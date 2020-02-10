using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bsoft.Common
{
    public class clsEncryptorDecryptor
    {
        private string password = "Bsoft";
        private System.Byte[] saltByte = System.Text.Encoding.ASCII.GetBytes("CMSSystem");

        private System.Byte[] keyByte = null;
        private System.Byte[] IVByte = null;

        private System.Security.Cryptography.RijndaelManaged myrijManaged;

        private void GenerateIVandKey()
        {
            System.Security.Cryptography.Rfc2898DeriveBytes myDeriveBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(password, saltByte);
            keyByte = myDeriveBytes.GetBytes(myrijManaged.KeySize / 8);
            IVByte = myDeriveBytes.GetBytes(myrijManaged.BlockSize / 8);
        }

        public string Decrypt(string CypherText)
        {
            string outText = string.Empty;
            System.Security.Cryptography.CryptoStream myCryptoStream = null;
            try
            {
                System.Byte[] data = System.Convert.FromBase64String(CypherText);
                System.IO.MemoryStream memStream = new System.IO.MemoryStream(data.Length);

                myrijManaged.Key = keyByte;
                myrijManaged.IV = IVByte;
                System.Security.Cryptography.ICryptoTransform myCryptoTransform = myrijManaged.CreateDecryptor();
                myCryptoStream = new System.Security.Cryptography.CryptoStream(memStream, myCryptoTransform, System.Security.Cryptography.CryptoStreamMode.Read);

                memStream.Write(data, 0, data.Length);
                memStream.Position = 0;
                outText = new System.IO.StreamReader(myCryptoStream).ReadToEnd();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (myCryptoStream != null)
                    myCryptoStream.Close();
            }
            return outText;
        }

        public string Encrypt(string Text)
        {
            string outText = string.Empty;
            System.Security.Cryptography.CryptoStream myCryptoStream = null;
            try
            {
                System.Byte[] bufferRead = System.Text.Encoding.ASCII.GetBytes(Text);
                System.IO.MemoryStream myOutMemStream = new System.IO.MemoryStream(1024);

                myrijManaged.Key = keyByte;
                myrijManaged.IV = IVByte;
                System.Security.Cryptography.ICryptoTransform myCryptoTransform = myrijManaged.CreateEncryptor();
                myCryptoStream = new System.Security.Cryptography.CryptoStream(myOutMemStream, myCryptoTransform, System.Security.Cryptography.CryptoStreamMode.Write);
                myCryptoStream.Write(bufferRead, 0, bufferRead.Length);
                myCryptoStream.FlushFinalBlock();
                System.Byte[] result = new byte[(int)myOutMemStream.Position];
                myOutMemStream.Position = 0;
                myOutMemStream.Read(result, 0, result.Length);
                outText = System.Convert.ToBase64String(result);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (myCryptoStream != null)
                    myCryptoStream.Close();
            }

            return outText;
        }

        public clsEncryptorDecryptor()
        {
            myrijManaged = new System.Security.Cryptography.RijndaelManaged();
            GenerateIVandKey();
        }
    }

    public static class StringCipher
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "tu89geji340t89u2";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}