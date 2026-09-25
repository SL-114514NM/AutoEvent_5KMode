using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues
{
    public static class TypingHelper
    {
        public static readonly HashSet<string> SelfClosingTags = new HashSet<string>()
        {
             "align", "allcaps", "alpha", "b", "color", "cspace", "font", "font-weight",
            "gradient", "i", "indent", "line-height", "line-indent", "link", "lowercase",
            "margin", "mark", "mspace", "nobr", "noparse", "page", "pos", "rotate", "s",
            "size", "smallcaps", "space", "sprite", "strikethrough", "style", "sub", "sup",
            "u", "uppercase", "voffset", "width",
        };
        public static List<string> BuildTypingSteps(string raw)
        {
            List<string> steps = new List<string>();

            if (string.IsNullOrEmpty(raw))
                return steps;

            // 记录已打开的标签，用于最后补闭合（LIFO）
            Stack<string> openTags = new Stack<string>();

            // 当前累积的文本（标签 + 已打出的可见字符）
            StringBuilder current = new StringBuilder(raw.Length + 16);

            int i = 0;
            int length = raw.Length;

            while (i < length)
            {
                char c = raw[i];

                // ---------- 处理 '<' ----------
                if (c == '<')
                {
                    // 判断是否在 noparse 中：在 noparse 里所有 '<' 都当普通字符
                    bool insideNoParse = IsInsideNoParse(openTags);

                    if (insideNoParse)
                    {
                        current.Append(c);
                        steps.Add(current.ToString());
                        i++;
                        continue;
                    }

                    int close = raw.IndexOf('>', i);

                    // 没有闭合的 '>'，当普通字符
                    if (close == -1)
                    {
                        current.Append(c);
                        steps.Add(current.ToString());
                        i++;
                        continue;
                    }

                    string tag = raw.Substring(i, close - i + 1);

                    // 判断是否像标签（<xxx> / </xxx> / <xxx=...>）
                    if (!IsLikelyTag(tag))
                    {
                        // 不像标签，当作普通可见字符逐个追加
                        for (int k = i; k <= close; k++)
                        {
                            current.Append(raw[k]);
                            steps.Add(current.ToString());
                        }
                        i = close + 1;
                        continue;
                    }

                    // ---------- 是标签 ----------
                    current.Append(tag);

                    if (tag.StartsWith("</"))
                    {
                        // 闭合标签：弹出对应的开始标签
                        string name = GetTagName(tag);
                        PopMatchingTag(openTags, name);
                    }
                    else if (tag.EndsWith("/>"))
                    {
                        // 自闭合形式 <xxx/>
                        // 不入栈
                    }
                    else
                    {
                        string name = GetTagName(tag);
                        if (!SelfClosingTags.Contains(name))
                        {
                            // 需要闭合，入栈保存原始标签（用于最后取名字）
                            openTags.Push(tag);
                        }
                    }

                    i = close + 1;
                    continue;
                }

                // ---------- 普通可见字符 ----------
                current.Append(c);
                steps.Add(current.ToString());
                i++;
            }

            // ---------- 补全所有未闭合标签 ----------
            if (openTags.Count > 0)
            {
                // 按 LIFO 顺序补闭合，保证嵌套正确
                // 栈顶是最后打开的，应该最先闭合
                StringBuilder closed = new StringBuilder(current.ToString());

                // Stack 枚举顺序是 LIFO，直接遍历即可
                foreach (string openTag in openTags)
                {
                    string name = GetTagName(openTag);
                    closed.Append("</").Append(name).Append(">");
                }

                if (steps.Count > 0)
                    steps[steps.Count - 1] = closed.ToString();
                else
                    steps.Add(closed.ToString());
            }

            return steps;
        }
        private static bool IsInsideNoParse(Stack<string> openTags)
        {
            foreach (string tag in openTags)
            {
                if (GetTagName(tag) == "noparse")
                    return true;
            }
            return false;
        }
        private static string GetTagName(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return string.Empty;

            int start = tag.StartsWith("</") ? 2 : 1;
            int end = tag.Length;

            for (int i = start; i < tag.Length; i++)
            {
                char c = tag[i];
                if (c == '=' || c == '>' || c == ' ' || c == '/' || c == '\t')
                {
                    end = i;
                    break;
                }
            }

            if (end <= start)
                return string.Empty;

            return tag.Substring(start, end - start).ToLowerInvariant();
        }

        private static bool IsLikelyTag(string tag)
        {
            if (string.IsNullOrEmpty(tag) || tag.Length < 3)
                return false;

            if (tag[0] != '<' || tag[tag.Length - 1] != '>')
                return false;

            int start = tag.StartsWith("</") ? 2 : 1;
            if (start >= tag.Length - 1)
                return false;

            char first = tag[start];
            if (!char.IsLetter(first))
                return false;

            // 检查 name 部分是否只由字母、数字、'-' 组成
            for (int i = start; i < tag.Length - 1; i++)
            {
                char c = tag[i];
                if (c == '=' || c == ' ' || c == '/' || c == '>')
                    break;

                if (!char.IsLetterOrDigit(c) && c != '-')
                    return false;
            }

            return true;
        }

        private static void PopMatchingTag(Stack<string> stack, string name)
        {
            if (stack.Count == 0 || string.IsNullOrEmpty(name))
                return;

            // 栈顶直接匹配，快速路径
            if (GetTagName(stack.Peek()) == name)
            {
                stack.Pop();
                return;
            }

            // 栈顶不匹配：从栈中找最近的同名标签并移除
            // 保留被跳过的标签（它们仍然处于打开状态）
            List<string> temp = new List<string>();
            bool found = false;

            while (stack.Count > 0)
            {
                string top = stack.Pop();
                if (GetTagName(top) == name)
                {
                    found = true;
                    break;
                }
                temp.Add(top);
            }

            // 把跳过的标签按原顺序放回
            for (int i = temp.Count - 1; i >= 0; i--)
                stack.Push(temp[i]);

            // found == false 表示没有匹配的开始标签，忽略这次闭合
            _ = found;
        }
    }
}
