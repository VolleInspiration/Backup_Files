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
                GetInformations();
                return;
            }
            else if (!args.Contains("-pathGet") || !args.Contains("-pathSet"))
            {
                Console.WriteLine("You cannot continue. Please set any informations in commandline args!");
                GetInformations();
                return;
            }
            else if(args.Length % 2 == 1) //0 == gerade anzahl der elemente des arrays ---- 1 == ungerade anzahl der elemente des arrays
            {
                Console.WriteLine("CommanlineArgs not valid. Please check and try again!");
                GetInformations();
                return;
            }
            
            Console.WriteLine("Initializing Backup Data");
            //get commandline args
            parameters = tools.GetCommandLineArgs(parameters);

            //get parameters values
            parameters.TryGetValue("-pathGet", out source);
            parameters.TryGetValue("-pathSet", out target);
            parameters.TryGetValue("-backupfolder", out backupFolder);
            parameters.TryGetValue("-projectname", out projectname);
            try
            {
                tools.CopyFilesRecursively(source, target + "\\" + backupFolder + "\\" + projectname);
                Console.WriteLine("press 'return' to continue...");    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: parsing cmd args");
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
            Console.ReadLine();
        }

        private static void GetInformations()
        {
            Console.WriteLine("You have to add some commandline args\n");
            Console.WriteLine("-pathGet \"PATH to source DIR\"");
            Console.WriteLine("-pathSet \"PATH to destination DIR\"");
            Console.WriteLine("optional:");
            Console.WriteLine("-backupfolder \"name your main backup folder\"");
            Console.WriteLine("-projectname \"name your current project\"");
        }

    }
}
