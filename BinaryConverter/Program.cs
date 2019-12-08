using System;

namespace BinaryConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Get your user input
            Console.Write("Enter a Decimal value: ");
            string userInput = Console.ReadLine();

            // Validate User Input
            string statusmsg;
            decimal decVal = 0;
            if (!userInputIsValid(userInput, out decVal, out statusmsg))
            {
                Console.WriteLine(statusmsg);
            }
            else
            {
                string binVal = string.Empty;
                // Check if it contains decimal
                if(decVal == Convert.ToInt32(decVal))
                {
                    binVal = ConvertIntToBinary(Convert.ToInt32(decVal));
                }
                else
                {
                    var userInputSplit = userInput.Split('.');

                    string intBinVal = string.Empty;
                    string decBinVal = string.Empty;

                    if (!string.IsNullOrEmpty(userInputSplit[0]))
                    {
                        intBinVal = ConvertIntToBinary(Convert.ToInt32(userInputSplit[0]));
                    }

                    if (!string.IsNullOrEmpty(userInputSplit[1]))
                    {
                        decBinVal = ConverFractionToBinary(userInputSplit[1]);
                    }
                    binVal = $"{intBinVal}.{decBinVal}";
                }

                //Return answer
                Console.WriteLine($"The binary equivalent value of decimal {userInput} is  {binVal}.");
            }
            Console.ReadKey(true);
        }

        /// <summary>
        /// Perform all neccessary validation here.
        /// </summary>
        /// <param name="userInput"></param>
        /// <param name="DecVal"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        private static bool userInputIsValid(string userInput, out decimal DecVal, out string errormsg)
        {
            bool isvalid = true;
            errormsg = string.Empty;
            if(!Decimal.TryParse(userInput, out DecVal))
            {
                errormsg = $"{userInput} is invalid! A decimal value is expected.";
                isvalid = false;
            }

            if (DecVal < 0)
            {
                errormsg = $"{userInput} is invalid! Value greater than Zero (0) is expected.";
                isvalid = false;
            }

            return isvalid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decVal"></param>
        /// <returns></returns>
        private static string ConverFractionToBinary(string decVal)
        {
            //0.23 = ((2 * 10^-1) + (3 * 10^-2))
            decimal decValConv = Convert.ToDecimal($"0.{decVal}");
            string retVal = string.Empty;
            int counter = 0;
            int rem = 0;
            do
            {
                decimal val = decValConv * 2;
                if (val == Convert.ToInt32(val))
                {
                    rem = 0;
                }
                else
                {
                    var valSplit = val.ToString().Split('.');
                    retVal += valSplit[0];
                    decValConv = Convert.ToDecimal($"0.{valSplit[1]}");
                }
                counter++;
            } while (rem != 0 || counter < 20 );
            return retVal;
        }

        /// <summary>
        /// Converts the integer part to binary
        /// </summary>
        /// <param name="decVal"></param>
        /// <returns></returns>
        private static string ConvertIntToBinary(int decVal)
        {
            string binVal = string.Empty;
            while (decVal > 1)
            {
                binVal = $"{decVal % 2}" + binVal;
                decVal /= 2;
            }
            return $"{decVal % 2}" + binVal;
        }
    }
}
