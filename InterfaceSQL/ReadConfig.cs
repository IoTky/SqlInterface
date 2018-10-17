using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InterfaceSQL
{
    public static class ReadConfig
    {
        public static void DeleteAllMark(string str, string mark)
        {
            int size = mark.Count();
            int pos = str.IndexOf(mark);
            if (pos > 0)
            {
                str.Remove(pos,size);
            }
        }

        public static void GetConfigInfoFromFile(string filePath,Dictionary<string,string> info)
        {
            if(!File.Exists(filePath))
            {
                Console.WriteLine("读取配置文件失败!");
                return;
            }
            string line;
            using (StreamReader stream = new StreamReader(filePath))
            {
                while((line = stream.ReadLine()) != null)
                {
                    int pos = line.IndexOf("\\");
                    if(pos == 0)
                    {
                        line.Remove(pos);
                    }
                    DeleteAllMark(line, "\"");
                    DeleteAllMark(line, " ");
                    DeleteAllMark(line, "\t");

                    pos = line.IndexOf("=");
                    if(pos == 0)
                    {
                        continue;
                    }

                    string key = line.Substring(0, pos);
                    string value = line.Substring(pos + 1);
                    int valueSize = value.Count();
                    if (valueSize > 0 && value[valueSize-1] == '\r')
                    {
                        value = value.Substring(0, valueSize - 1);
                    }
                    if (key.Length != 0 && value.Length != 0)
                    {
                        info[key] = value;
                    }
                }
            }
        }

    }
}
