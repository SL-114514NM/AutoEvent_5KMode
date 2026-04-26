using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class Config
    {
        public bool IsEnabled { get; set; }
        [Description("是否启动5K模式")]

        public bool Is5KMode { get; set; } = false;
        [Description("我直接用DeepSeek生成学术问题，后续改成自己的，要不然我没钱了qwq")]
        public string DeepSeekKey { get; set; } = "";
    }
}

