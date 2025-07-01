﻿namespace pps_api.Utils
{
    public static class PasswordHashHelper
    {
        public static string Hash(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public static bool Verify(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);
    }

}
