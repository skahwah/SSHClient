using System;
using System.IO;
using Renci.SshNet;

namespace SSHClient.Modules
{
    public class Transfer
    {
        public void Download(SshClient con, SftpClient client, string source, string destination)
        {
            try
            {
                // download directory has to exist before moving forward
                if (Directory.Exists(destination))
                {
                    ExecuteCommand ExecuteCommand = new ExecuteCommand();
                    string file = ExecuteCommand.Command(con, "ls " + source);

                    // check to see if the file exists on the SSH server before downloading it
                    if (!file.ToLower().Contains(source.ToLower()))
                    {
                        Console.WriteLine("[!] Failed! '" + source + "' does not exist on the remote host.");
                        return;
                    }

                    Console.Out.WriteLine("[+] Downloading '" + source + "' to '" + destination + "'\n");
                    int pos = source.LastIndexOf("/") + 1;
                    string downloadFile = source.Substring(pos, source.Length - pos);
                    string pathLocalFile = Path.Combine(destination, downloadFile);
                    Stream fileStream = File.OpenWrite(pathLocalFile);
                    client.DownloadFile(source, fileStream);
                    client.Disconnect();

                    if (File.Exists(pathLocalFile))
                    {
                        Console.WriteLine("[+] File downloaded to '" + pathLocalFile + "'");

                    }
                    else
                    {
                        Console.WriteLine("[!] Failed! Can not download '" + source + "' to '" + destination + "'");
                    }
                }
                else
                {
                    Console.WriteLine("[!] Failed! '" + destination + "' is not a valid directory.");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("[!] Failed! Can not download '" + source + "' to '" + destination + "'");
                return;
            }
        }

        public void Upload(SshClient con, SftpClient client, string source, string destination)
        {
            try
            {
                // check to see if the file exists locally before moving forwards
                if (File.Exists(source))
                {
                    Console.Out.WriteLine("\n[+] Uploading '" + source + "' to '" + destination + "'\n");
                    int pos = source.LastIndexOf("\\") + 1;
                    string uploadFile = source.Substring(pos, source.Length - pos);
                    string pathRemoteFile = destination + "/" + uploadFile;

                    using (FileStream fileStream = File.OpenRead(source))
                    {
                        client.UploadFile(fileStream, pathRemoteFile);
                        client.Disconnect();
                    }

                    ExecuteCommand ExecuteCommand = new ExecuteCommand();
                    string file = ExecuteCommand.Command(con, "ls " + pathRemoteFile);

                    // check to see if the file exists on the SSH server
                    if (file.ToLower().Contains(pathRemoteFile.ToLower()))
                    {
                        Console.WriteLine("[+] File uploaded to '" + pathRemoteFile + "'");
                    }
                    else
                    {
                        Console.WriteLine("[!] Failed! '" + pathRemoteFile + "' does not exist on the remote host.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("[!] Failed! '" + source + "' is not a valid file.");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("[!] Failed! Can not upload '" + source + "' to '" + destination + "'");
                return;
            }
        }
    }
}