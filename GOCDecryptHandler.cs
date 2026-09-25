using LabApi.Features.Wrappers;
using ProjectMER.Features.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues
{
    public class GOCDecryptHandler
    {
        /// <summary>
        /// RGM密钥破解情况
        /// key为密钥机ID, value为该密钥机是否已通过验证
        /// </summary>
        public static Dictionary<int, bool> RGMMY = new Dictionary<int, bool>();
        /// <summary>
        /// RGM密钥破解机情况
        /// key为密钥机ID, value为该密钥机的正确密钥
        /// </summary>
        public static Dictionary<int, string> MiYaos = new Dictionary<int, string>();
        public static void OnHandler(Player Target,int MiyaoID,string InputMsg)
        {
            RGMMY[MiyaoID] = true;
        }
        /// <summary>
        /// RGM密钥破解机列表
        /// key为破解机原理图实例, value为暂时唯一ID
        /// </summary>
        public static Dictionary<SchematicObject, int> MiYaoJis = new Dictionary<SchematicObject, int>();
        public static void RegiaterAll()
        {

        }
    }
}
