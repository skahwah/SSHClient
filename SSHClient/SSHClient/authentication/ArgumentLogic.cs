using System;
using System.Collections.Generic;
using Renci.SshNet;
using SSHClient.Auth;
using SSHClient.Modules;

namespace SSHClient.Auth
{
    public class ArgumentLogic
    {
        //variables used for command line arguments and general program execution
        private static SshClient con = null;
        private static SftpClient scon = null;
        private static String authType = "";
        private static String host = "";
        private static String port = "";
        private static String user = "";
        private static String pass = "";
        private static String module = "";
        private static String command = "";
        private static String source = "";
        private static String destination = "";


        public void AuthenticationType(Dictionary<string, string> argDict)
        {
            // if authentication type is not given, display help and return
            if (!argDict.ContainsKey("a"))
            {
                Console.WriteLine("\n[!] ERROR: Must supply an authentication type (-a Password or -a Key)");
                return;
            }

            /* if authentication type is password, make sure that:
                - the SSH server
                - port on the SSH server
                - username 
                - and password has been given, otherwise display help and return
            */
            if (argDict["a"].ToLower().Equals("password") && argDict.ContainsKey("s") && argDict.ContainsKey("p") && argDict.ContainsKey("u") && argDict.ContainsKey("k") && argDict.ContainsKey("m"))
            {
                authType = argDict["a"].ToLower();
                user = argDict["u"];
                pass = argDict["k"];
                host = argDict["s"];
                port = argDict["p"];

                PasswordAuthentication PasswordAuthentication = new PasswordAuthentication();
                con = PasswordAuthentication.SSH(host, Convert.ToInt32(port), user, pass);
                EvaluateTheArguments(argDict);
            }
            else if (argDict["a"].ToLower().Equals("password"))
            {
                Console.WriteLine("\n[!] ERROR: Must supply a SSH server (-s), username (-u), password (-k), port (-p) and module (-m)");
                return;
            }

            /* if authentication type is key, make sure that:
                - the SSH server
                - port on the SSH server
                - username 
                - and path to key has been given, otherwise display help and return
            */
            if (argDict["a"].ToLower().Equals("key") && argDict.ContainsKey("s") && argDict.ContainsKey("p") && argDict.ContainsKey("u") && argDict.ContainsKey("k") && argDict.ContainsKey("m"))
            {
                authType = argDict["a"].ToLower();
                user = argDict["u"];
                pass = argDict["k"];
                host = argDict["s"];
                port = argDict["p"];

                KeyAuthentication KeyAuthentication = new KeyAuthentication();
                con = KeyAuthentication.SSH(host, Convert.ToInt32(port), user, pass);
                EvaluateTheArguments(argDict);
            }
            else if (argDict["a"].ToLower().Equals("key"))
            {
                Console.WriteLine("\n[!] ERROR: Must supply a SSH server (-s), username (-u), path to key (-k), port (-p) and module (-m)");
                return;
            }
        }

        // EvaluateTheArguments
        public static void EvaluateTheArguments(Dictionary<string, string> argDict)
        {
            // if the module type is command, then set the module to command and set option to the actual command
            if (argDict["m"].ToLower().Equals("command"))
            {
                if (!argDict.ContainsKey("c"))
                {
                    Console.WriteLine("\n[!] ERROR: Must supply a command (-c)");
                    return;
                }
                else 
                {
                    module = argDict["m"].ToLower();
                    command = argDict["c"];
                } 
            }
            // if the module type is download, then set the module to download and set the source and destination variables
            else if (argDict["m"].ToLower().Equals("download"))
            {
                if (!argDict.ContainsKey("f") && !argDict.ContainsKey("d"))
                {
                    Console.WriteLine("\n[!] ERROR: Must supply a the remote file you want to download (-f) and local download directory (-d)");
                    return;
                }

                else
                {
                    module = argDict["m"].ToLower();
                    source = argDict["f"];
                    destination = argDict["d"];
                }
            }
            // if the module type is upload, then set the module to upload and set the source and destination variables
            else if (argDict["m"].ToLower().Equals("upload"))
            {
                if (!argDict.ContainsKey("f") && !argDict.ContainsKey("d"))
                {
                    Console.WriteLine("\n[!] ERROR: Must supply a the local file you want to upload (-f) and remote destination directory (-d)");
                    return;
                }
                else
                {
                    module = argDict["m"].ToLower();
                    source = argDict["f"];
                    destination = argDict["d"];
                }
            }
            else
            {
                module = argDict["m"].ToLower();
            }


            // this is effectively a huge module switch
            if (module.Equals("connect"))
            {
                if (con.ToString() == "Renci.SshNet.SshClient" && con.IsConnected)
                {
                    Console.WriteLine("[+] Success! " + user + " can log in to " + host + ":" + port + "\n");
                    con.Disconnect();
                    }
            }
            else if (module.Equals("command"))
            {
                Console.Out.WriteLine("[+] Executing: '" + command + "'\n");
                ExecuteCommand ExecuteCommand = new ExecuteCommand();
                Console.WriteLine(ExecuteCommand.Command(con, command));
            }
            else if (module.Equals("download"))
            {
                Transfer Transfer = new Transfer();

                if (authType == "password")
                {
                    PasswordAuthentication PasswordAuthentication = new PasswordAuthentication();
                    scon = PasswordAuthentication.Sftp(host, Convert.ToInt32(port), user, pass);
                }
                else
                {
                    KeyAuthentication KeyAuthentication = new KeyAuthentication();
                    scon = KeyAuthentication.Sftp(host, Convert.ToInt32(port), user, pass);
                }

                Transfer.Download(con, scon, source, destination);
            }
            else if (module.Equals("upload"))
            {
                Transfer Transfer = new Transfer();

                if (authType == "password")
                {
                    PasswordAuthentication PasswordAuthentication = new PasswordAuthentication();
                    scon = PasswordAuthentication.Sftp(host, Convert.ToInt32(port), user, pass);
                }
                else
                {
                    KeyAuthentication KeyAuthentication = new KeyAuthentication();
                    scon = KeyAuthentication.Sftp(host, Convert.ToInt32(port), user, pass);
                }

                Transfer.Upload(con, scon, source, destination);
            }
            else
            {
                Console.WriteLine("[!] ERROR: Module " + module + " does not exist\n");
            }

        } 
    }
}