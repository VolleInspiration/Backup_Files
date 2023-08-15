using System;
using System.Collections.Generic;
using System.Text;

namespace BackupCopyFiles
{
    public class ErrorLog
    {
        public static ErrorLog Instance { get; }

        private string _fewArgs = "Too few args set!";
        public string FewArgs { get { return _fewArgs; } }

        private string _argsIncomplete = "You cannot continue. Please set any information in commandline args!";
        public string ArgsIncomplete { get { return _argsIncomplete; } }

        private string _argsInvalid = "CommandlineArgs not valid. Please check and try again!";
        public string ArgsInvalid { get { return _argsInvalid; } }

        private string _invalidPath = "Invalid path!";
        public string InvalidPath { get { return _invalidPath; } }
    }
}
