using System;

namespace SSHClient.Modules
{
    public class Help
    {
        public Help()
        {
            initialize();
        }

        // this returns the help menu to console
        public void initialize()
        {
            Console.WriteLine("SShClient.exe v2.0");
            Console.WriteLine("");

            Console.WriteLine("Examples:");
            Console.WriteLine("SSHClient.exe -a password -s a.server.com -p 443 -u ubuntu -k Password123! -m connect");
            Console.WriteLine("SSHClient.exe -a key -s 192.168.0.5 -p 22 -u skawa -k c:\\Users\\skawa\\pri.key -m connect");
            Console.WriteLine("SSHClient.exe -a password -s a.server.com -p 443 -u ubuntu -k Password123! -m command -c \"ls /\"");
            Console.WriteLine("SSHClient.exe -a key -s 192.168.0.5 -p 22 -u skawa -k c:\\Users\\skawa\\pri.key -m download -f \"/root/.ssh/id_rsa\" -d \"C:\\users\\skawa\\downloads\"");
            Console.WriteLine("SSHClient.exe -a key -s 192.168.0.5 -p 22 -u skawa -k c:\\Users\\skawa\\pri.key -m upload -f \"C:\\users\\skawa\\downloads\\payload.py\" -d \"/home/ubuntu\"");
            Console.WriteLine("");

            Console.WriteLine("Authentication Type (-a)");
            Console.WriteLine("");

            Console.WriteLine("-a Password - Use password authentication.");
            Console.WriteLine("\t[+] -u USERNAME | SSH username");
            Console.WriteLine("\t[+] -k PASSWORD | Password of SSH user");
            Console.WriteLine("\t[+] -s SERVER | SSH server to connect to");
            Console.WriteLine("\t[+] -p PORT | Port on the SSH server to connect to");
            Console.WriteLine("");

            Console.WriteLine("-a Key - Use key based authentication.");
            Console.WriteLine("\t[+] -u USERNAME | SSH username");
            Console.WriteLine("\t[+] -k KEY | Path to the SSH key");
            Console.WriteLine("\t[+] -s SERVER | SSH server to connect to");
            Console.WriteLine("\t[+] -p PORT | Port on the SSH server to connect to");
            Console.WriteLine("");

            Console.WriteLine("Standard Modules (-m)");
            Console.WriteLine("\t[+] connect | Test to see if the credentials work");
            Console.WriteLine("\t[+] command -c COMMAND | Execute an arbitary command");
            Console.WriteLine("\t[+] download -f REMOTEFILE -d LOCALPATH | Download a file to a local path");
            Console.WriteLine("\t[+] upload -f LOCALFILE -d REMOTEPATH | Upload a file to a remote path");
        }
    }
}