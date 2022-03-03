using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RoomManagement.Security {

    public class PasswordManager {

        private const int SALT_SIZE = 24;
        private const int ITERATIONS = 30;
        private const int HASH_SIZE = 24;
        private static readonly byte[] SALT = new byte[SALT_SIZE];
        private string FilePath { get; }

        internal PasswordManager(string filePath) {
            this.FilePath = filePath;
        }

        static PasswordManager() {
            
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            provider.GetBytes(SALT);

        }
        internal static string HashPassword(string roomPin) {
            
            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(roomPin, SALT, ITERATIONS);

            byte[] bArrHashedPwd = hash.GetBytes(HASH_SIZE);

            return Encoding.UTF8.GetString(bArrHashedPwd, 0, bArrHashedPwd.Length);
            
        }

        internal bool ValidatePassword(string password) {

            string hashedPassword = HashPassword(password);

            try {

                string[] passwordHashes = File.ReadAllLines(this.FilePath);

                return passwordHashes.Contains(hashedPassword);

            }
            catch (IOException e) {
                
                Console.WriteLine(e);
                throw;
            }

        }

        internal void SetPassword(string password) {

            string hashedPassword = HashPassword(password);

            try {
                
                // If file does not yet exist, create file then write hashed password to it
                if (!File.Exists(this.FilePath)) {

                    using StreamWriter pwdFile = File.CreateText(this.FilePath);
                    pwdFile.WriteLine(hashedPassword);
                    
                }
            
                // If file already exists, simply write hashed password to it
                else {
                    
                    using StreamWriter pwdFile = new (this.FilePath, true);
                    pwdFile.WriteLineAsync(hashedPassword);
                    
                }
                
            }
            
            catch (IOException e) {
                
                Console.WriteLine(e);
                throw;
                
            }

        }

    }

}
