using System;
using System.Security.Cryptography;
using System.Text;

namespace GamePool.BLL.Core.Helpers
{
    internal static class StringExtensions
    {
        public static string ComputeSha512Hash(this string input)
        {
            var alg = SHA512.Create();

            alg.ComputeHash(Encoding.UTF8.GetBytes(input));

            return BitConverter.ToString(alg.Hash).Replace("-", string.Empty);
        }
    }
}