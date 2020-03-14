using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;

namespace OrcasTeam.Shandard.Libary.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        ///     使用Zip压缩字符串
        /// </summary>
        /// <param name="text">压缩字符串</param>
        /// <returns>压缩后的字符串</returns>
        public static string Compress(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var inputBytes = Encoding.UTF8.GetBytes(text);
            using var outStream = new MemoryStream();
            BZip2.Compress(new MemoryStream(inputBytes), outStream, true, 2);
            outStream.Close();
            return Convert.ToBase64String(outStream.ToArray());
        }

        /// <summary>
        ///  反压缩字符串
        /// </summary>
        /// <param name="compressedString"></param>
        /// <returns></returns>
        public static string DeCompress(this string compressedString)
        {
            if (string.IsNullOrEmpty(compressedString)) return compressedString;
            var inputBytes = Convert.FromBase64String(compressedString);
            using var outStream = new MemoryStream();
            BZip2.Decompress(new MemoryStream(inputBytes), outStream, true);
            outStream.Close();
            return Encoding.UTF8.GetString(outStream.ToArray());
        }
    }
}