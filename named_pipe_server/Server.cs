using System;
using System.IO;
using System.IO.Pipes;

namespace named_pipe_server
{
    class Server
    {
        static void Main(string[] args)
        {
            string pipeName = "mypipe";
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName,
                PipeDirection.Out))
            {
                Console.WriteLine("pipe object created, wait for client connection.");
                pipeServer.WaitForConnection();
                Console.WriteLine("Client connected.");
                try
                {
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;
                        string send = Console.ReadLine();
                        sw.WriteLine(send as object);
                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("exception:" + e.ToString());
                }
            }
        }
    }
}
