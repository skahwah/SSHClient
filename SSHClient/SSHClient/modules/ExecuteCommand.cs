using System;
using Renci.SshNet;

namespace SSHClient.Modules
{
    public class ExecuteCommand
    {
        public void Command(SshClient client, string command)
        {
            try
            {
                var cmd = client.CreateCommand(command);
                var result = cmd.Execute();
                Console.Write(result);
                client.Disconnect();
            }
            catch
            {
                Console.WriteLine("[!] Failed! can not execute " + command);
                Environment.Exit(0);
            }
        } 
    }
}