# SSHClient
A small SSH client written in C#. 

# Usage
You can grab a copy of SSHClient from the [releases](https://github.com/skahwah/SSHClient/releases) page. Alternatively, feel free to compile the solution yourself. Fody/Costura has been used to embed the [SSH.NET](https://github.com/sshnet/SSH.NET) reference.

## Mandatory Arguments
The mandatory arguments consist of an authentication type (either Windows or Local), connection parameters and a module.

* <b>-a</b> - Authentication Type
  * <b>-a Password</b> - Use password authentication.
  * <b>-a Key</b> - Use key based authentication. 

If the authentication type is <b>Password</b>, then you will need to supply the following parameters.
  * <b>-u USERNAME</b> - SSH username
  * <b>-k PASSWORD</b> - Password of SSH user
  * <b>-s SERVER</b> - SSH server to connect to
  * <b>-p PORT</b> - Port on the SSH server to connect to

If the authentication type is <b>Key</b>, then you will need to supply the following parameters.
  * <b>-u USERNAME</b> - SSH username
  * <b>-k KEY</b> - Path to the SSH key
  * <b>-s SERVER</b> - SSH server to connect to
  * <b>-p PORT</b> - Port on the SSH server to connect to

## Standard Modules
Standard modules are used to interact against a single MS SQL server.

* <b>connect</b> - Test to see if the credentials work
* <b>command -o COMMAND</b> - Execute an arbitrary command
* <b>download -o PATH</b> - Download a file to `C:\\Users\\Username\\Documents\\`

## Examples
### Test connection
```
SSHClient.exe -a Key -s 18.191.138.12 -p 22 -u ubuntu -k c:\users\skawa\aws.key -m connect
[+] Success! ubuntu can log in to 18.191.138.12:22
```

### Execute commands
```
SSHClient.exe -a Key -s 18.191.138.12 -p 22 -u ubuntu -k c:\users\skawa\aws.key -m command -o "ps -eaf"

[+] Executing: ps -eaf
UID          PID    PPID  C STIME TTY          TIME CMD
root           1       0  0 Nov10 ?        00:00:12 /sbin/init
root           2       0  0 Nov10 ?        00:00:00 [kthreadd]
root           3       2  0 Nov10 ?        00:00:00 [rcu_gp]
root           4       2  0 Nov10 ?        00:00:00 [rcu_par_gp]
root           5       2  0 Nov10 ?        00:00:48 [kworker/0:0-events]
root           6       2  0 Nov10 ?        00:00:00 [kworker/0:0H-events_highpri]
root           9       2  0 Nov10 ?        00:00:00 [mm_percpu_wq]
root          10       2  0 Nov10 ?        00:00:00 [rcu_tasks_rude_]
<snipped>
```

### Download a file
```
SSHClient.exe -a Key -s 18.191.138.12 -p 22 -u ubuntu -k c:\users\skawa\aws.key -m download -o /home/ubuntu/file.txt

[+] Downloading: /home/ubuntu/file.txt
[+] File downloaded to: C:\Users\skawa\Documents\file.txt
```

## Roadmap
The below techniques are on the roadmap for future releases
* Upload files
* Perhaps add an option to customize the the downloads directory, this is currently `C:\\Users\\Username\\Documents\\`
* Authentication based on users session

