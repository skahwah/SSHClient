using System;
using System.IO;
using Renci.SshNet;

namespace SSHClient.Auth
{
    public class KeyAuthentication
    {
        // this handles SSH password authentication 
        public SshClient SSH(String host, int port, String username, String key)
        {
            TestAuthentication TestAuthentication = new TestAuthentication();

            var stream = new FileStream(key, FileMode.Open, FileAccess.Read);
            var file = new PrivateKeyFile(stream);
            var authMethod = new PrivateKeyAuthenticationMethod(username, file);
            var connectionInfo = new ConnectionInfo(host, port, username, new PrivateKeyAuthenticationMethod(username, file));
            return TestAuthentication.SSH(host, port, username, connectionInfo);
        } // end Send

        public SftpClient Sftp(String host, int port, String username, String key)
        {
            TestAuthentication TestAuthentication = new TestAuthentication();

            var stream = new FileStream(key, FileMode.Open, FileAccess.Read);
            var file = new PrivateKeyFile(stream);
            var authMethod = new PrivateKeyAuthenticationMethod(username, file);
            var connectionInfo = new ConnectionInfo(host, port, username, new PrivateKeyAuthenticationMethod(username, file));
            return TestAuthentication.Sftp(host, port, username, connectionInfo);
        } // end Send
    }
}