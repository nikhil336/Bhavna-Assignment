using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();
            PrintDateFormat();
            CalculateDays();
            Console.ReadLine();
        }

        /// <summary>
        /// Method calls GetValidFromDate() and GetValidToDate() methods to get valid dates from user and calls the CalculateDaysBetweenFromDateAndToDate() to calculate days between the dates
        /// </summary>
        internal static void CalculateDays()
        {
            Console.Write( "Enter From Date: " );
            DateTime fromDate = GetValidFromDate();
            Console.Write( "Enter To Date: " );
            DateTime toDate = GetValidToDate( fromDate );
            CalculateDaysBetweenFromDateAndToDate( fromDate, toDate );
        }
        
        /// <summary>
        /// It prints the general application information
        /// </summary>
        internal static void GetAppInfo()
        {
            string appName = "Days Calculator";
            string appVersion = "1.0.0";
            string author = "Nikhil Rana";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( $"{appName}: Version {appVersion} by {author}" );
            Console.ResetColor();
        }

        /// <summary>
        /// It prints the valid date format
        /// </summary>
        internal static void PrintDateFormat()
        {
            Console.WriteLine( "Date Format: (DD/MM/YYYY)" );
        }

        /// <summary>
        /// Method takes input from the user until valid 'From date' is entered and returns the DateTime object
        /// </summary>
        /// <returns>Valid DateTime Object</returns>
        internal static DateTime GetValidFromDate()
        {
            DateTime fromDateObject;

            while ( !( DateTime.TryParse( Console.ReadLine(), out fromDateObject ) && fromDateObject < DateTime.Today ) )
            {
                if ( !(fromDateObject < DateTime.Today) )
                {
                    Console.WriteLine("The FROM date should be less than today's date");
                }
                else
                {
                    Console.WriteLine( "Please Enter valid date!" );
                }

                Console.Write( "From Date: " );
            }

            return fromDateObject;
        }

        /// <summary>
        /// Method takes input from the user until valid 'To date' is entered and it also accepts the 'From date' object as parameter to validate the 'To date' and returns the DateTime object
        /// </summary>
        /// <param name="fromDateObject">'From date' object is required to validate the 'To date'</param>
        /// <returns>Valid DateTime Object</returns>
        internal static DateTime GetValidToDate( DateTime fromDateObject )
        {
            DateTime toDateObject;

            while ( !( DateTime.TryParse( Console.ReadLine(), out toDateObject ) && toDateObject >= fromDateObject ) )
            {
                if ( toDateObject != new DateTime() && !(toDateObject >= fromDateObject) )
                {
                    Console.WriteLine( "The TO date should be greater than or equal to FROM date" );
                }
                else
                {
                    Console.WriteLine( "Please Enter valid date!" );
                }

                Console.Write( "To Date: " );
            }

            return toDateObject;
        }

        /// <summary>
        /// Method takes two DateTime objects as input and calculates the numbers of days present between them and output the result 
        /// </summary>
        /// <param name="fromDate">'From date' Object</param>
        /// <param name="toDate">'To date' Object</param>
        internal static void CalculateDaysBetweenFromDateAndToDate( DateTime fromDate, DateTime toDate )
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan timeSpan = toDate - fromDate;
            int years = (zeroTime + timeSpan).Year - 1;
            int months = (zeroTime + timeSpan).Month - 1;
            int days = (zeroTime + timeSpan).Day - 1;

            Console.Write( "Result: " );
            if ( years == 0 && months == 0 )
            {
                Console.WriteLine( $"{days} day(s)" );
            }
            else if ( years == 0 )
            {
                Console.WriteLine( $"{months} month(s) {days} day(s)" );
            }
            else
            {
                Console.WriteLine( $"{years} year(s) {months} month(s) {days} day(s)" );
            }
        }
    }
}
