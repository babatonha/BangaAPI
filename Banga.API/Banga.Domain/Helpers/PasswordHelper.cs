namespace Banga.Domain.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateRandomPassword()
        {
            const string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string smallLetters = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%^&*()-_=+[{]};:'\",<.>/?";

            var random = new Random();
            var password = new char[8];

            password[0] = capitalLetters[random.Next(capitalLetters.Length)];
            password[1] = smallLetters[random.Next(smallLetters.Length)];
            password[2] = numbers[random.Next(numbers.Length)];
            password[3] = specialCharacters[random.Next(specialCharacters.Length)];
            var remainingCharacters = capitalLetters + smallLetters + numbers + specialCharacters;
            for (var i = 4; i < password.Length; i++)
            {
                password[i] = remainingCharacters[random.Next(remainingCharacters.Length)];
            }

            random.Shuffle(password);

            return new string(password);
        }
    }
}
