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
        private static String option = "";

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
            // standard single sql server logic
            // if the module type is command, then set the module to command and set option to the actual command
            if (argDict["m"].ToLower().Equals("command") && !argDict.ContainsKey("o"))
            {
                Console.WriteLine("\n[!] ERROR: Must supply a command (-o)");
                module = argDict["m"].ToLower();
                return;
            }
            else if (argDict.ContainsKey("o"))
            {
                module = argDict["m"].ToLower();
                option = argDict["o"];
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
                    Console.WriteLine("[+] Success! " + user + " can log in to " + host + ":" + port);
                    con.Disconnect();
                    }
            }
            else if (module.Equals("command"))
            {
                Console.Out.WriteLine("\n[+] Executing: " + option);
                ExecuteCommand ExecuteCommand = new ExecuteCommand();
                ExecuteCommand.Command(con, option);
            }
            else if (module.Equals("download"))
            {
                Console.Out.WriteLine("\n[+] Downloading: " + option);
                DownloadFile DownloadFile = new DownloadFile();

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

                DownloadFile.Download(scon, option);
            }
            else
            {
                Console.WriteLine("[!] ERROR: Module " + module + " does not exist\n");
            }

        } // end EvaluateTheArguments
    }
}