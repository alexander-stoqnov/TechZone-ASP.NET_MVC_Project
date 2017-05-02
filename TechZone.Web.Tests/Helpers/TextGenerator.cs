namespace TechZone.Web.Tests.Helpers
{
    using System;
    using System.Text;

    public static class TextGenerator
    {
        public static string RandomUsernameGenerator()
        {
            string letters = "abcdefghijklmnopqrstuvwyzABCDEFGHIJKLMNOPQRSTUVWYZ";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(letters[rnd.Next(0, 42)]);
            }

            return sb.ToString();
        }

        public static string RandomPasswordGenerator()
        {
            string smallLetters = "abcdefghijklmnopqrstuvwyz";
            string bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
            string digits = "0123456789";
            string specialCharacters = "!?@#$%^&*(_)[]{}";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                sb.Append(smallLetters[rnd.Next(0, 24)]);
            }
            sb.Append(bigLetters[rnd.Next(0, 23)]);
            sb.Append(digits[rnd.Next(0, 9)]);
            sb.Append(specialCharacters[rnd.Next(0, 15)]);

            return sb.ToString();
        }
    }
}