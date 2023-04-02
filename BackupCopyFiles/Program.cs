using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupCopyFiles
{
    class Program
    {
        static Dictionary<string, string> parameters = new Dictionary<string, string>();

        static Tools tools = new Tools();
        static string source = default, 
            target = default, 
            backupFolder = default, 
            projectname = default;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(GetInformations());
                return;
            }
            else if (!args.Contains("-pathGet") || !args.Contains("-pathSet"))
            {
                Console.WriteLine("You cannot continue. Please set any information in commandline args!");
                Console.WriteLine(GetInformations());
                return;
            }
            else if(args.Length % 2 == 1) //0 == gerade anzahl der elemente des arrays ---- 1 == ungerade anzahl der elemente des arrays
            {
                Console.WriteLine("CommandlineArgs not valid. Please check and try again!");
                Console.WriteLine(GetInformations());
                return;
            }

            Console.WriteLine("Initializing Backup Data\n");

            //get commandline args
            parameters = tools.GetCommandLineArgs(parameters);

            //get parameters values
            parameters.TryGetValue("-pathGet", out source);
            parameters.TryGetValue("-pathSet", out target);
            parameters.TryGetValue("-backupfolder", out backupFolder);
            parameters.TryGetValue("-projectname", out projectname);

            if (!IsPathValid())
            {
                Console.WriteLine("Invalid path!\n");
                Console.WriteLine( GetInformations() );
                return;
            }

            Console.WriteLine("> source directory: {0}\n> target directory: {1}\n", source, target);

            tools.CopyFilesRecursively(source, Path.Combine(target, backupFolder, projectname));
            Console.WriteLine("press 'return' to continue...");
            Console.ReadLine();
        }

        private static bool IsPathValid()
        {
            return Directory.Exists(source) && Directory.Exists(target) ? true : false;
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
