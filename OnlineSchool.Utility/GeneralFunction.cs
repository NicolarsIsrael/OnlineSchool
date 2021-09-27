using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnlineSchool.Utility
{
    public class GeneralFunction
    {
        public static string GeneratePassword()
        {
            Random random = new Random();
            string lowerAlpha = "abcdefghijklmnopqrstuvwxyz";
            string upperAlpha = lowerAlpha.ToUpper();
            string numbers = "0123456789";
            string characters = "!@#$%^&*()+=[]{}:;/<>?";
            string password = "";
            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    int randomChar = random.Next(0, upperAlpha.Length);
                    password = password + upperAlpha[randomChar];
                }
                else if (i == 2)
                {
                    int randomChar = random.Next(0, lowerAlpha.Length);
                    password = password + lowerAlpha[randomChar];
                }
                else if (i == 3)
                {
                    int randomChar = random.Next(0, numbers.Length);
                    password = password + numbers[randomChar];
                }
                else if (i == 4)
                {
                    int randomChar = random.Next(0, characters.Length);
                    password = password + characters[randomChar];
                }
                else
                {
                    int type = random.Next(1, 5);
                    if (type == 1)
                    {
                        int p = random.Next(0, lowerAlpha.Length);
                        password = password + lowerAlpha[p];
                    }
                    else if (type == 2)
                    {
                        int p = random.Next(0, upperAlpha.Length);
                        password = password + upperAlpha[p];
                    }
                    else
                    {
                        int p = random.Next(0, numbers.Length);
                        password = password + numbers[p];

                    }
                }

            }
            return password;
        }

        public static string GetUrlPath(string url)
        {
            return $"~/{url.Remove(0, 8)}";
        }

        public static string DateInString(DateTime date, bool includeTime= true)
        {
            if (includeTime)
                return date.ToString("dd/MMM/yyyy - hh:mm:ss");
            else
                return date.ToString("dd/MMM/yyyy");
        }

        private static string key = "e5519d1b310d4ff1a15ec326c3425175";
        private static string appID = "ca856a.vidyo.io";
        private static long expiresInSecs = 1800;
        private static string expiresAt = null;
        private const long EPOCH_SECONDS = 62167219200;

        public static string GenerateVideoMeetingToken(string userName)
        {

            // As long as proper arguments were entered, generate the token
            if ((appID != null) && (key != null) && (userName != null))
            {
                string expires = "";

                // Check if using expiresInSecs or expiresAt
                if (expiresInSecs > 0)
                {
                    TimeSpan timeSinceEpoch = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                    expires = (Math.Floor(timeSinceEpoch.TotalSeconds) + EPOCH_SECONDS + expiresInSecs).ToString();
                }
                else if (expiresAt != null)
                {
                    try
                    {
                        TimeSpan epochToExpires = DateTime.Parse(expiresAt).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                        expires = (Math.Floor(epochToExpires.TotalSeconds) + EPOCH_SECONDS).ToString();
                    }
                    catch (Exception e)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }


                if (expiresAt != null)
                    Console.WriteLine("Setting expiresAt     : " + expiresAt);
                Console.WriteLine("Expirey time          : " + expires);
                string jid = userName + "@" + appID;
                string body = "provision" + "\0" + jid + "\0" + expires + "\0" + "";

                var encoder = new UTF8Encoding();
                var hmacsha = new HMACSHA384(encoder.GetBytes(key));
                byte[] mac = hmacsha.ComputeHash(encoder.GetBytes(body));

                // macBase64 can be used for debugging
                //string macBase64 = Convert.ToBase64String(hashmessage);

                // Get the hex version of the mac
                string macHex = BytesToHex(mac);

                string serialized = body + '\0' + macHex;
                return Convert.ToBase64String(encoder.GetBytes(serialized));
                //Console.WriteLine("\nGenerated token:\n" + Convert.ToBase64String(encoder.GetBytes(serialized)));
            }
            else
            {
                throw new Exception();
            }
        }

        private static string BytesToHex(byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

    }
}
