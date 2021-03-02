using System;
using System.Collections.Generic;
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

    }
}
