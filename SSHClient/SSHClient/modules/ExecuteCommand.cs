using System;
using Renci.SshNet;

namespace SSHClient.Modules
{
    public class ExecuteCommand
    {
        public string Command(SshClient client, string command)
        {
            try
            {
                var cmd = client.CreateCommand(command);
                string result = cmd.Execute();
                client.Disconnect();
                return result;
            }
            catch
            {
                return "[!] Failed! can not execute " + command;
            }
        } 
    }
}