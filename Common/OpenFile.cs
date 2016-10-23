using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public class OpenFile
    {
        public static void ReadSettings(Dictionary<string, string> settingDict)
        {
            string inputStr = "";
            using (FileStream fs = new FileStream("pipe.config", FileMode.Open))
            {
                int a = -1;
                while ((a = fs.ReadByte()) != -1)
                {
                    inputStr += Convert.ToChar(a);
                }
            }
            string[] splitKey = new string[] { System.Environment.NewLine };
            string[] settings = inputStr.Split(splitKey, StringSplitOptions.None);
            foreach (var i in settings)
            {
                string[] value = i.Split('=');
                settingDict[value[0]] = value[1];
                Console.WriteLine(i);
            }
        }

        public static string ReadJson()
        {
            using (StreamReader sr = new StreamReader("test.json"))
            {
                string fileContent = sr.ReadToEnd();
                return fileContent;
            }
        }
    }
}
