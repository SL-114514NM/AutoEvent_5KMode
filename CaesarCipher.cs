using AutoEvent_5KMode.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues
{
    public class CaesarCipher
    {
        /// <summary>
        /// 今日凯撒密钥
        /// </summary>
        public static int ToDayKey = Plugin.Instance.Config.CaesarKey;
        public static string Encrypt(string plaintext, int key)
        {
            char[] buffer = plaintext.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((letter + key - offset) % 26 + offset);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }
        public static string Decrypt(string ciphertext, int key)
        {
            return Encrypt(ciphertext, 26 - (key % 26));
        }
    }
}
