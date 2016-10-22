using System;
using System.IO;
using System.IO.Pipes;

namespace named_pipe_client
{
    class Clinet
    {
        static void Main(string[] args)
        {
            string serverName = "", pipeName = "";
            ReadSettings(ref serverName, ref pipeName);
            PipeClient(serverName, pipeName);
        }

        static void ReadSettings(ref string serverName, ref string pipeName)
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
                if ("serverName" == value[0])
                {
                    serverName = value[1];
                }
                else if ("pipeName" == value[0])
                {
                    pipeName = value[1];
                }
                Console.WriteLine(i);
            }
        }

        static void PipeClient(string serverName, string pipeName)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(serverName,
                pipeName, PipeDirection.In))
            {
                pipeClient.Connect();
                using (StreamReader sr = new StreamReader(pipeClient))
                {
                    string temp;
                    while ((temp = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Received from server:{0}", temp);
                    }
                }
            }
        }
    }
}
