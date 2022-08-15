using System;
using System.Collections.Generic;
using System.IO;

namespace ExportDump
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ensure user has specified arguments.
            if (args.Length != 1)
            {
                Console.WriteLine(@"[!] Must specify filename argument.");
                Console.WriteLine(@"Usage: .\ExportDump.exe C:\Windows\System32\version.dll");
                return;
            }

            // Attempt to open user-specified DLL.
            string filepath = args[0];
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"[!] Could not open '{filepath}'. Ensure you have permissions to read the specified file.");
                return;
            }


            // Dump exported functions
            string exports = "";
            PeNet.PeFile pe = new PeNet.PeFile(filepath);
            Console.WriteLine($"[*] Dumping all {pe.ExportedFunctions.Length} exported functions from {filepath}.");
            foreach (PeNet.Header.Pe.ExportFunction exportFunction in pe.ExportedFunctions)
            {
                exports += String.Format("#pragma comment(linker, \"/export:{0}={1}.{0},@{2}\")\n",
                    exportFunction.Name, 
                    Path.GetDirectoryName(filepath).Replace("\\", "\\\\") + "\\\\" + Path.GetFileNameWithoutExtension(filepath), 
                    exportFunction.Ordinal
                );
            }
            
            Console.WriteLine($"[+] Dumping done. Check pragma.h");
            File.WriteAllText("pragma.h", exports);
        }
    }
}
