using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public static class ListExtension
    {
        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("列表不能为空");

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                int index = Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % list.Count;
                return list[index];
            }
        }
    }
}
