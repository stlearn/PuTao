using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PuTao
{
    class Program
    {
        //最大字符长度
        private static int maxLength = 0;
        //最大value值
        private static int maxValue = 0;

        //首行标志
        private static bool firstLine = true;
        public static void Main(string[] args)
        {
            try
            {
                var n = Convert.ToInt32(Console.ReadLine());
                var inputString = Convert.ToString(Console.ReadLine());

                var indexLabel = inputString.Split(' ')[0];
                var order = inputString.Split(' ')[1];

                Dictionary<string, Int32> dic = new Dictionary<string, int>();

                //读入数据
                for (int i = 0; i < n; i++)
                {
                    inputString = Convert.ToString(Console.ReadLine());
                    dic.Add(inputString.Split(' ')[0], Convert.ToInt32(inputString.Split(' ')[1]));
                    //统计最大字符串长度
                    if (inputString.Split(' ')[0].Length > maxLength)
                    {
                        maxLength = inputString.Split(' ')[0].Length;
                    }
                    //统计最大value值
                    if (Convert.ToInt32(inputString.Split(' ')[1]) > maxValue)
                    {
                        maxValue = Convert.ToInt32(inputString.Split(' ')[1]);
                    }
                }
                //按name排序
                if (indexLabel.ToLower() == "name")
                {
                    //降序
                    if (order.ToLower() == "desc")
                    {
                        Print(dic, true, true);
                    }
                    //升序
                    else
                    {
                        Print(dic, true, false);
                    }
                }
                //按value排序
                else
                {
                    //降序
                    if (order.ToLower() == "desc")
                    {
                        Print(dic, false, true);
                    }
                    //升序
                    else
                    {
                        Print(dic, false, false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Print(Dictionary<string, int> dic, bool byName, bool isDesc)
        {
            if (byName)
            {
                PrintByName(dic, isDesc);
            }
            else
            {
                PrintByValue(dic, isDesc);
            }
        }

        private static void PrintByName(Dictionary<string, int> dic, bool isDesc)
        {
            int value;
            while (dic.Count != 0)
            {
                var Names = dic.Keys.ToArray();
                var aName = Names[0];
                foreach (var a in Names)
                {
                    if (isDesc ? (aName.CompareTo(a) <= 0) : (aName.CompareTo(a) >= 0))
                    {
                        aName = a;
                    }
                }
                if (dic.TryGetValue(aName, out value))
                {
                    //移除已经输出的
                    dic.Remove(aName);
                    //可视化输出
                    PrintItem(aName, value, dic.Count == 0);
                }
                else
                {
                    throw (new Exception("字典错误"));
                }
            }
        }
        private static void PrintByValue(Dictionary<string, int> dic, bool isDesc)
        {
            int value;
            while (dic.Count != 0)
            {
                var Names = dic.Keys.ToArray();
                var aName = Names[0];
                int v1, v2;
                foreach (var a in Names)
                {
                    if (dic.TryGetValue(aName, out v1) && dic.TryGetValue(a, out v2))
                    {
                        if (isDesc ? (v1 <= v2) : (v1 >= v2))
                        {
                            aName = a;
                        }
                    }
                    else
                    {
                        throw (new Exception("字典错误"));
                    }
                }
                if (dic.TryGetValue(aName, out value))
                {
                    //移除已经输出的
                    dic.Remove(aName);
                    //可视化输出
                    PrintItem(aName, value, dic.Count == 0);
                }
                else
                {
                    throw (new Exception("字典错误"));
                }
            }
        }

        private static void PrintItem(string name, int value, bool lastLine)
        {
            string line;
            if (firstLine)
            {
                line = "\u250c";
                for (int i = 0; i < maxLength; i++)
                {
                    line += "\u2500";
                }
                line += "\u252c";
                for (int i = 0; i < 20; i++)
                {
                    line += "\u2500";
                }
                line += "\u2510";

                Console.WriteLine(line);
                firstLine = false;
            }

            //数据行
            line = "\u2502";
            for (int i = 0; i < maxLength - name.Length; i++)
            {
                line += " ";
            }

            string chart = "";
            int length = (value * 20) / maxValue;
            for (int i = 0; i < length; i++)
            {
                chart += "\u2588";
            }
            Console.WriteLine(line + $"{name}\u2502{chart,-20}\u2502");


            //最后一行
            if (lastLine)
            {
                line = "\u2514";
                for (int i = 0; i < maxLength; i++)
                {
                    line += "\u2500";
                }
                line += "\u2534";
                for (int i = 0; i < 20; i++)
                {
                    line += "\u2500";
                }
                line += "\u2518";
                Console.WriteLine(line);

                return;
            }


            //中间线
            line = "\u251c";
            for (int i = 0; i < maxLength; i++)
            {
                line += "\u2500";
            }
            line += "\u253c";
            for (int i = 0; i < 20; i++)
            {
                line += "\u2500";
            }
            line += "\u2524";
            Console.WriteLine(line);
        }
    }
}
