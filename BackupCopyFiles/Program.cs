using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace BackupCopyFiles
{
    public class Program
    {
        static Dictionary<string, string> parameters = new Dictionary<string, string>();

        static Tools tools = new Tools();
        static string source = default, 
            target = default, 
            backupFolder = default, 
            projectname = default;
        
        public static void Main(string[] args)
        {
            var error = new ErrorLog();

            if (!validateArgs(args)) 
                return;
            
            Console.WriteLine("Initializing Backup Data\n");

            //get commandline args
            parameters = tools.GetCommandLineArgs(parameters);

            //get parameters values
            parameters.TryGetValue("-pathGet", out source);
            parameters.TryGetValue("-pathSet", out target);
            parameters.TryGetValue("-backupfolder", out backupFolder);
            parameters.TryGetValue("-projectname", out projectname);

            if(!IsPathValid(source))
            {
                Console.WriteLine($"{error.InvalidPath} \n");
                Console.WriteLine(GetInformations());
                return;
            }
            if (!IsPathValid(target))
            {
                Directory.CreateDirectory(target);
            }

            Console.WriteLine("> source directory: {0}\n> target directory: {1}\n", source, target);

            tools.CopyFilesRecursively(source, Path.Combine(target, backupFolder, projectname));
            Console.WriteLine("press 'return' to continue...");
            Console.ReadLine();
        }

        private static bool validateArgs(string[] args)
        {
            var error = new ErrorLog();

            if (args.Length == 0)
            {
                Console.WriteLine($"{error.FewArgs} \n");
                Console.WriteLine(GetInformations());
                return false;
            }
            else if (!args.Contains("-pathGet") || !args.Contains("-pathSet"))
            {
                Console.WriteLine($"{error.ArgsIncomplete} \n");
                Console.WriteLine(GetInformations());
                return false;
            }
            else if (args.Length % 2 == 1) //0 == gerade anzahl der elemente des arrays ---- 1 == ungerade anzahl der elemente des arrays
            {
                Console.WriteLine($"{error.ArgsInvalid} \n");
                Console.WriteLine(GetInformations());
                return false;
            }
            return true;
        }

        private static bool IsPathValid(string path)
        {
            return Directory.Exists(path);
        }

        private static string GetInformations()
        {
            return "You have to add some commandline args\n" +
                "-pathGet \"PATH to source DIR\"\n" +
                "-pathSet \"PATH to destination DIR\"\n" +
                "optional:\n" +
                "-backupfolder \"name your main backup folder\"\n" +
                "-projectname \"name your current project\"";
        }

    }
}
