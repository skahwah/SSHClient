
using System;
using Renci.SshNet;

namespace SSHClient.Auth
{
    public class PasswordAuthentication
    {
        // this handles SSH password authentication 
        public SshClient SSH(String host, int port, String username, String password)
        {
            TestAuthentication TestAuthentication = new TestAuthentication();
            var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
            return TestAuthentication.SSH(host, port, username, connectionInfo);
        } // end SSH

        public SftpClient Sftp(String host, int port, String username, String password)
        {
            TestAuthentication TestAuthentication = new TestAuthentication();
            var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
            return TestAuthentication.Sftp(host, port, username, connectionInfo);
        } // end SSH
    }
}