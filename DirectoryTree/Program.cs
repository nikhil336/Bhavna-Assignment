using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirectoryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();
            DirectoryInfo userDirectory = GetDirectoryInfo();
            PrintDirectoryTree( userDirectory );
            Console.ReadLine();
        }

        /// <summary>
        /// Method takes directory path as input from user until valid directory path is entered and then returns the DirectoryInfo object of that path
        /// </summary>
        /// <returns>DirectoryInfo object</returns>
        internal static DirectoryInfo GetDirectoryInfo()
        {
            Console.WriteLine( "Enter Directory Path:" );
            DirectoryInfo userDirectory = new DirectoryInfo( Console.ReadLine() );

            while ( !userDirectory.Exists )
            {
                Console.WriteLine( "Enter valid directory path:" );
                userDirectory = new DirectoryInfo( Console.ReadLine() );
            }

            return userDirectory;
        }

        /// <summary>
        /// Method prints the hierarchical structure of all the files and directories that are present in DirectoryInfo object i.e. passed by the program
        /// </summary>
        /// <param name="directory">DirectoryInfo object</param>
        internal static void PrintDirectoryTree( DirectoryInfo directory )
        {
            Console.WriteLine( $"--{directory.Name}" );
            DirectoryInfo[] directoryInfos = directory.GetDirectories();

            for ( int i = 0; i < directoryInfos.Length; i++ )
            {
                PrintDirectoryTree( directoryInfos[i] );
            }

            FileInfo[] fileInfos = directory.GetFiles();

            for ( int i = 0; i < fileInfos.Length; i++ )
            {
                Console.WriteLine( $"-{fileInfos[i].Name}" );
            }
        }

        /// <summary>
        /// It prints the general application information
        /// </summary>
        internal static void GetAppInfo()
        {
            string appName = "Directory Tree";
            string appVersion = "1.0.0";
            string author = "Nikhil Rana";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( $"{appName}: Version {appVersion} by {author}" );
            Console.ResetColor();
        }
    }
}
