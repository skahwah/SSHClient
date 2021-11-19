using System;
using Renci.SshNet;

namespace SSHClient.Auth
{
    public class TestAuthentication
    {
        public SshClient SSH(string host, int port, string username, ConnectionInfo connectionInfo)
        {
            SshClient client = new SshClient(connectionInfo);

            try
            {
                client.Connect();
                return client;
            }
            catch
            {
                Console.WriteLine("[!] Failed! " + username + " can not log in to " + host + ":" + port);
                Environment.Exit(0);
                return null;
            }
        } // end SSH

        public SftpClient Sftp(string host, int port, string username, ConnectionInfo connectionInfo)
        {
            SftpClient client = new SftpClient(connectionInfo);

            try
            {
                client.Connect();
                return client;
            }
            catch
            {
                Console.WriteLine("[!] Failed! " + username + " can not log in to " + host + ":" + port);
                Environment.Exit(0);
                return null;
            }
        } // end Sftp
    }
}