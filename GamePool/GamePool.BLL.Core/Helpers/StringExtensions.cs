using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.BLL.Core.Helpers
{
    internal static class StringExtensions
    {
        public static string ComputeSHA256Hash(this string input)
        {
            SHA512 alg = SHA512.Create();

            alg.ComputeHash(Encoding.UTF8.GetBytes(input));

            return BitConverter.ToString(alg.Hash).Replace("-", string.Empty);
        }
    }
}