using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalizeWords
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAppInfo();
            CapitalizeWords();
            Console.ReadLine();
        }

        /// <summary>
        /// Method takes a sentence as input from user and then captialize first character of each word leaving conjuctions, prepositions and articles and then outputs the sentence
        /// </summary>
        internal static void CapitalizeWords()
        {
            Console.WriteLine( "Enter Sentence:" );
            string sentence = Console.ReadLine();
            List<string> conjunctions = new List<string>() { "and", "or", "but", "nor", "yet", "so", "for", "a", "an", "the", "in", "to", "of", "at", "by", "up", "off", "on" };
            Dictionary< char, List< int > > dictionary = new Dictionary< char, List< int > >();
            int length = sentence.Length;

            try
            {
                sentence = RemoveSymbols( sentence, dictionary );
                string[] words = sentence.Split(' ');
                sentence = string.Empty;

                for ( int i = 0 ; i < words.Length; i++ )
                {
                    if ( conjunctions.Contains( words[i] ) )
                    {
                        sentence += words[i] + " ";
                    }
                    else
                    {
                        if (words[i].Length != 0)
                        {
                            sentence += words[i][0].ToString().ToUpper() + words[i].Substring(1, words[i].Length - 1) + " ";
                        }
                        else
                        {
                            sentence += " ";
                        }
                    }
                }

                Console.WriteLine( AddSymbolsBack( sentence, dictionary, length ) );
            }
            catch (Exception e)
            {
                Console.WriteLine( $"Exception occured while capitalizing words: {e.Message}" );
            }
        }

        /// <summary>
        /// Method removes characters like ',', '.', '?' from the sentence and save their indexes in dictionary object and then returns it
        /// </summary>
        /// <param name="sentence">string</param>
        /// <param name="dictionary">Dictionary<char,List<int>> object</param>
        /// <returns>string sentence</returns>
        internal static string RemoveSymbols( string sentence, Dictionary< char, List< int > > dictionary )
        {
            string result = string.Empty;

            try
            {
                for (int i = 0; i < sentence.Length; i++)
                {
                    if (!(sentence[i] == ',' || sentence[i] == '.' || sentence[i] == '?'))
                    {
                        result += sentence[i];
                    }
                    else
                    {
                        if (dictionary.ContainsKey(sentence[i]))
                        {
                            List<int> list = dictionary[sentence[i]];
                            list.Add(i);
                            dictionary[sentence[i]] = list;
                        }
                        else
                        {
                            List<int> list = new List<int>();
                            list.Add(i);
                            dictionary.Add(sentence[i], list);
                        }
                    }
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( $"Exception occured while removing symbols from sentence: {e.Message}" );
            }

            return result;
        }

        /// <summary>
        /// Method add backs the characters at their original positions in the sentence using the indexes that are present in dictionary object and then returns it
        /// </summary>
        /// <param name="sentence">string sentence</param>
        /// <param name="dictionary">Dictionary object</param>
        /// <param name="length">Length of original sentence</param>
        /// <returns>string sentence</returns>
        internal static string AddSymbolsBack(string sentence, Dictionary<char, List<int>> dictionary, int length )
        {
            string result = string.Empty;
            List<int> indexes1 = new List<int>();
            List<int> indexes2 = new List<int>();
            List<int> indexes3 = new List<int>();

            try
            {
                if (dictionary.ContainsKey(','))
                {
                    indexes1 = dictionary[','];
                }
                if (dictionary.ContainsKey('.'))
                {
                    indexes2 = dictionary['.'];
                }
                if (dictionary.ContainsKey('?'))
                {
                    indexes3 = dictionary['?'];
                }

                for (int i = 0, j = 0; i < length; i++)
                {
                    if (indexes1.Contains(i))
                    {
                        result += ',';
                    }
                    else if (indexes2.Contains(i))
                    {
                        result += '.';
                    }
                    else if (indexes3.Contains(i))
                    {
                        result += '?';
                    }
                    else
                    {
                        result += sentence[j++];
                    }
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( $"Exception occured while adding back symbols into sentence: {e.Message}" );
            }

            return result;
        }

        /// <summary>
        /// It prints the general application information
        /// </summary>
        internal static void GetAppInfo()
        {
            string appName = "Capitalize Words";
            string appVersion = "1.0.0";
            string author = "Nikhil Rana";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( $"{appName}: Version {appVersion} by {author}" );
            Console.ResetColor();
        }
    }
}
