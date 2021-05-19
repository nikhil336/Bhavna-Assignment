using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BufferDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();
            GetInputs();
            Console.ReadLine();
        }

        /// <summary>
        /// Method takes inputs from the user until '?' is entered and store it in a list and if the list is full then it asks for users permission to replace the old values.
        /// </summary>
        internal static void GetInputs()
        {
            Console.WriteLine( "Enter any value or ? to show output" );
            List<string> values = new List<string>( 10 );
            int i = 0;
            while ( true )
            {
                Console.Write( "Value: " );
                string value = Console.ReadLine();
                if ( value.Equals( "?" ) )
                {
                    PrintList( values );
                    break;
                }
                else if (values.Count < 10)
                {
                    values.Add(value);
                }
                else
                {
                    Console.WriteLine( $"List is full do you want to remove an old value to add {value}? (YES/NO)" );
                    if( Console.ReadLine().ToUpper().Equals( "YES" ) )
                    {
                        if ( i == 10 )
                        {
                            i = 0;
                        }
                        values[ i++ ] = value;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Method takes list object as a parameter then prints that list
        /// </summary>
        /// <param name="list">List object</param>
        internal static void PrintList(List<string> list)
        {
            if ( list.Count == 0 )
            {
                Console.WriteLine( "List is empty!" );
            }
            else
            {
                Console.WriteLine("Values:-");
                for( int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine( list[i] );
                }
            }
        }

        /// <summary>
        /// It prints the general application information
        /// </summary>
        internal static void GetAppInfo()
        {
            string appName = "List of 10 values";
            string appVersion = "1.0.0";
            string author = "Nikhil Rana";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{appName}: Version {appVersion} by {author}");
            Console.ResetColor();
        }
    }
}
