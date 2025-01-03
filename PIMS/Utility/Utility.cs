using System;
using System.Security.Cryptography;
using System.Text;

namespace PIMS.Utility;


public static class Utility
{
    public static byte[] ComputeSha256Hash(string rawData)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return bytes;
    }

    public static bool CompareHash(this string rawData, byte[] hash)
    {
        return ComputeSha256Hash(rawData).SequenceEqual(hash);
    }
}
