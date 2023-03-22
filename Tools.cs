using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace BackupCopyFiles
{
    class Tools
    {
        public Dictionary<string,string> GetCommandLineArgs(Dictionary<string, string> parameters)
        {
            Console.WriteLine("Get CommandLine Args\n");
            string[] CommandLineArgs = Environment.GetCommandLineArgs();
            parameters = new Dictionary<string, string>
            {
                { "-pathGet", "" },
                { "-pathSet", "" },
                { "-backupfolder", "" },
                { "-projectname", "" }
            };

            if (CommandLineArgs.Length > 1)
            {
                for (int i = 0; i < CommandLineArgs.Length; i++)
                {
                    if (CommandLineArgs[i].StartsWith("-pathGet"))
                        parameters["-pathGet"] += CommandLineArgs[i + 1];
                    if (CommandLineArgs[i].StartsWith("-pathSet"))
                        parameters["-pathSet"] += CommandLineArgs[i + 1];
                    if (CommandLineArgs[i].StartsWith("-backupfolder"))
                        parameters["-backupfolder"] += CommandLineArgs[i + 1];
                    if (CommandLineArgs[i].StartsWith("-projectname"))
                        parameters["-projectname"] += GetTimestamp(DateTime.Now) + "_" + CommandLineArgs[i + 1];
                }
                if (parameters["-projectname"] == "")
                    parameters["-projectname"] += GetTimestamp(DateTime.Now) + "_DEMOPROJECT";
            }
            return parameters;
        }

        public void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            Console.WriteLine("Copy files - starting...\n");
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            int index = 0,
                percentage = 0,
                length = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).Length;
            long dirSize = GetDirectorySize(sourcePath);
            long copiedSize = 0L;
            FileInfo fi = null;
            
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                ++index;
                percentage = 100 * index / length;
                fi = new FileInfo(newPath);
                copiedSize += fi.Length;
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                Console.Write("\r{0}/{1}   {2}%   {3}MB copied", index, length, percentage, copiedSize / 1024 / 1024);
            }
            Console.WriteLine("\n\nCopy files - {0}MB - done!", dirSize / 1024 / 1024);
        }

        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmss");
        }
    }
}
