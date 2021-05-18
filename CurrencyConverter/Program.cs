using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();
            startCurrencyConvertor();
            Console.ReadLine();
        }

        /// <summary>
        /// Method when it runs for the first time it calls AddCurrencies() method and when the method runs for another time then it asks user if want to add new values or not and it also fetchs the currency values for the database and calls the CalculateCurrency method for further process
        /// </summary>
        internal static void startCurrencyConvertor()
        {
            List<Currency> currencies = GetCurrenciesFromDatabase();

            if( currencies.Count == 0 )
            {
                Console.WriteLine( "Enter Currency names and conversion rates:-" );
                AddCurrencies();
                currencies = GetCurrenciesFromDatabase();
            }
            else
            {
                Console.WriteLine( "Do want to add new Conversion rates? (YES/NO)" );
                if ( Console.ReadLine().ToUpper().Equals( "YES" ) )
                {
                    deleteCurrenciesFromDatabase();
                    Console.WriteLine( "Enter Currency names and conversion rates:" );
                    AddCurrencies();
                    currencies = GetCurrenciesFromDatabase();
                }
            }

            CalculateCurrency( currencies );
        }

        /// <summary>
        /// Method takes currency symbol and amount inputs from the user then checks if the currency is present in database or not and then output the converted amount 
        /// </summary>
        /// <param name="currencies">List<Currency> object</param>
        internal static void CalculateCurrency( List<Currency> currencies )
        {
            Console.Write( "Enter currency symbol (eg. EURINR): " );
            string currencySymbol = Console.ReadLine().ToUpper();

            while ( currencySymbol.LastIndexOf( "INR" ) == -1 )
            {
                Console.Write("Please enter valid currency symbol (eg. EURINR): ");
                currencySymbol = Console.ReadLine().ToUpper();
            }

            string currencyName = currencySymbol.Remove( currencySymbol.LastIndexOf( "INR" ) );

            while ( !currencies.Any( c => currencyName.Equals( c.Name ) ) )
            {
                Console.Write( "This Currency is not present please enter another (eg. EURINR): " );
                currencySymbol = Console.ReadLine().ToUpper();
                currencyName = currencySymbol.Remove( currencySymbol.LastIndexOf( "INR" ) );
            } 

            Console.Write( "Enter Amount: " );
            double amount;

            while ( !double.TryParse( Console.ReadLine(), out amount ) )
            {
                Console.Write( "Please enter a valid amount: " );
            }

            Currency currency = currencies.First( c => currencyName.Equals( c.Name ) );
            Console.WriteLine( $"Amount in INR: { String.Format("{0:0.00}", currency.Rate * amount) }" );
        }

        /// <summary>
        /// Method takes currency names and conversion rates as inputs from user and then add them into the database and if values entered are more than 5 then it asks user if he wants to add more values or not
        /// </summary>
        internal static void AddCurrencies()
        {
            List<Currency> currencies = new List<Currency>();
            for ( int i = 0; ; i++ )
            {
                Console.Write( "Currency name: " );
                string name = Console.ReadLine().ToUpper();

                while ( currencies.Any( c => c.Name.Equals(name) ) )
                {
                    Console.WriteLine("This Currency already present please enter another!");
                    Console.Write("Currency name: ");
                    name = Console.ReadLine();
                }

                currencies.Add( new Currency() { Name = name } );
                Console.Write( "Conversion rate: " );
                double rate;

                while ( !Double.TryParse( Console.ReadLine(), out rate ) )
                {
                    Console.WriteLine( "Enter valid rate" );
                }

                AddCurrencyIntoDatabase( name, rate );

                if ( i > 5 )
                {
                    Console.WriteLine( "Do you want to add more currencies? (YES/NO)" );
                    string ans = Console.ReadLine();
                    if ( ans.ToUpper().Equals( "YES" ) )
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method empties the currency table when it is called
        /// </summary>
        internal static void deleteCurrenciesFromDatabase()
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection( "data source=.; database=Practice; integrated security=SSPI" );
                SqlCommand sqlCommand = new SqlCommand( "delete from currency;", sqlConnection );
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine( $"Exception occured while deleting data: {e.Message}" );
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method takes two parameters currency name and conversion rate and then adds them into the database
        /// </summary>
        /// <param name="name">Currency name</param>
        /// <param name="rate">Conversion rate</param>
        internal static void AddCurrencyIntoDatabase( string name, double rate )
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection( "data source=.; database=Practice; integrated security=SSPI" );
                SqlCommand sqlCommand = new SqlCommand( $"insert into currency values('{name.ToUpper()}',{rate});", sqlConnection );
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch ( Exception e )
            {
                Console.WriteLine( $"Exception occured while inserting data: {e.Message}" );
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Method forms a list of currencies present in currency table and returns it
        /// </summary>
        /// <returns>List<Currency> object</returns>
        internal static List<Currency> GetCurrenciesFromDatabase()
        {
            SqlConnection sqlConnection = null;
            List<Currency> currencies = new List<Currency>();

            try
            {
                sqlConnection = new SqlConnection( "data source=.; database=Practice; integrated security=SSPI" );
                SqlCommand sqlCommand = new SqlCommand( "select * from currency;", sqlConnection );
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while ( sqlDataReader.Read() )
                {
                    Currency currency = new Currency();
                    currency.Name = sqlDataReader["name"].ToString();
                    currency.Rate = Double.Parse( sqlDataReader["rate"].ToString() );
                    currencies.Add( currency );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( $"Exception occured while retrieving data: {e.Message}" );
            }
            finally
            {
                sqlConnection.Close();
            }

            return currencies;
        }

        /// <summary>
        /// It prints the general application information
        /// </summary>
        internal static void GetAppInfo()
        {
            string appName = "Currency Convertor";
            string appVersion = "1.0.0";
            string author = "Nikhil Rana";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( $"{appName}: Version {appVersion} by {author}" );
            Console.ResetColor();
        }
    }

    class Currency
    {
        public string Name { get; set; }
        public double Rate { get; set; }
    }
}
