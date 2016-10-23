using System;
using System.IO;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Diagnostics;
using Common;

namespace named_pipe_client
{
    class Clinet
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> settingDict = new Dictionary<string, string>();
            OpenFile.ReadSettings(settingDict);
            const string serverName = "serverName";
            const string pipeName = "pipeName";
            Debug.Assert(settingDict.ContainsKey(serverName));
            Debug.Assert(settingDict.ContainsKey(pipeName));
            PipeClient(settingDict[serverName], settingDict[pipeName]);
        }

        static void PipeClient(string serverName, string pipeName)
        {
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(serverName,
                pipeName, PipeDirection.Out))
            {
                Console.WriteLine("Wait for pipe server connection://{0}/{1}", serverName, pipeName);
                pipeClient.Connect();
                Console.WriteLine("Connected to server.");
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.AutoFlush = true;
                    string send = OpenFile.ReadJson();
                    sw.WriteLine(send as object);
                }
                //using (StreamReader sr = new StreamReader(pipeClient))
                //{
                //    string temp;
                //    while ((temp = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine("Received from server:{0}", temp);
                //    }
                //}
            }
        }
    }
}
