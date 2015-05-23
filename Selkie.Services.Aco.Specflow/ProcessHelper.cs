using System;
using System.Diagnostics;
using System.Management;

namespace Selkie.Services.Aco.Specflow
{
    public class ProcessHelper
    {
        public string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;

            var searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach ( ManagementBaseObject baseObject in processList )
            {
                var obj = ( ManagementObject ) baseObject;

                object[] argList =
                {
                    string.Empty,
                    string.Empty
                };

                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner",
                                                                 argList));
                if ( returnVal == 0 )
                {
                    return argList [ 1 ] + "\\" + argList [ 0 ]; // return DOMAIN\user
                }
            }

            return "NO OWNER";
        }

        public void KillProcessByNameAndUser(string processName)
        {
            Process[] foundProcesses = Process.GetProcessesByName(processName);
            Console.WriteLine(foundProcesses.Length + " processes found.");

            foreach ( Process p in foundProcesses )
            {
                p.Kill();
            }
        }
    }
}