using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MarketPracticingPlatform.Service.Crypto
{
    public static class DataCrypt
    {
        public static string Convert(string str)

        {

            MD5 hashMaker = MD5.Create();

            byte[] hash = hashMaker.ComputeHash(Encoding.Unicode.GetBytes(str));

            StringBuilder sBuilder = new StringBuilder();

            foreach (var b in hash)

                sBuilder.Append(b.ToString("x2"));

            return sBuilder.ToString();

        }
    }
}
