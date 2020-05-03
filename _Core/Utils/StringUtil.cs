using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _Core.Utils
{
    public class StringUtil
    {
        /// <summary>
        /// 从包含中英文的字符串中截取固定长度的一段，inputString为传入字符串，len为截取长度（一个汉字占两个位）。
        /// </summary>
        public static string CutString(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return "";
            }

            inputString = inputString.Trim();
            byte[] myByte = Encoding.Default.GetBytes(inputString);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < inputString.Length; i++)
                {
                    byte[] tempByte = Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + "...";
            }
            else
            {
                return inputString;
            }
        }

        /// <summary>
        /// 去除文本中的html代码
        /// </summary>
        public static string RemoveHtml(string inputString)
        {
            var tostring = Regex.Replace(inputString, "&nbsp;", "");
            return Regex.Replace(tostring, @"<[^>]+>", "");
        }
    }
}
