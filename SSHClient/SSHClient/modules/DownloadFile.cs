using System;
using System.IO;
using Renci.SshNet;

namespace SSHClient.Modules
{
    public class DownloadFile
    {
        public void Download(SftpClient client, string download)
        {
            try
            {
                int pos = download.LastIndexOf("/") + 1;
                string downloadFile = download.Substring(pos, download.Length - pos);
                string pathLocalFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), downloadFile);
                Stream fileStream = File.OpenWrite(pathLocalFile);
                client.DownloadFile(download, fileStream);
                Console.WriteLine("[+] File downloaded to: " + pathLocalFile);
                client.Disconnect();
            }
            catch
            {
                Console.WriteLine("[!] Failed! can not download " + download);
                Environment.Exit(0);
            }
        } 
    }
}