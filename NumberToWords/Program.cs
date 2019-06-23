using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter the number from 0 to 999999999 to be written in words:");
                String inputNumber = String.Empty;
                int inputNumberlength = 0;
                int number=1;
                String numberInwordsString = String.Empty;
                String placeValue = String.Empty;
                NumberToWordConverter numToWordObj = new NumberToWordConverter();
                inputNumber = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(inputNumber))
                {
                    throw (new IsNullOrWhiteSpaceException());
                }
                else
                {
                    //to remove trailing and leading white spaces for the input number
                    inputNumber = inputNumber.Trim();
                }

                bool isValidNumber =int.TryParse(inputNumber, out number);

                if (isValidNumber)
                {
                    Console.WriteLine("Below is the entered number in words.");
                    Console.WriteLine();
                    if(Convert.ToInt32(inputNumber) == 0)
                    {
                        Console.WriteLine("zero");
                    }
                    else
                    {
                        inputNumber = inputNumber.TrimStart(new char[] { '0' });
                        if (Convert.ToInt32(inputNumber) > 999999999)
                            Console.WriteLine("The entered value is invalid.");
                        else
                        {
                            inputNumberlength = inputNumber.Length;
                            placeValue = inputNumberlength > 6 ? "Million" : (inputNumberlength > 3) ? "Thousand" : "Hundred";
                            switch(placeValue)
                            {
                                case "Hundred":
                                    numberInwordsString = numToWordObj.FormWordsInHundred(inputNumber);
                                    break;
                                case "Thousand":
                                    numberInwordsString = numToWordObj.FormWordsInHundred(inputNumber.Substring(0,(inputNumberlength-3))) + " thousand " + numToWordObj.FormWordsInHundred(inputNumber.Substring(inputNumberlength-3).TrimStart(new char[] {'0'}));
                                    break;
                                case "Million":
                                    numberInwordsString = numToWordObj.FormWordsInHundred(inputNumber.Substring(0, (inputNumberlength - 6))) + " million " + numToWordObj.FormWordsInHundred(inputNumber.Substring((inputNumberlength - 6),3).TrimStart(new char[] { '0' })) + " thousand " + numToWordObj.FormWordsInHundred(inputNumber.Substring(inputNumberlength - 3).TrimStart(new char[] { '0' }));
                                    break;
                            }
                            Console.WriteLine(numberInwordsString.Trim());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The entered value is invalid.");
                }
                
            }
            catch (IsNullOrWhiteSpaceException nullException)
            {
                Console.WriteLine("The entered value is empty.");
            }
            catch(System.FormatException ex)
            {
                Console.WriteLine("Please Enter the Valid Number " + ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            finally
            {
                Console.WriteLine();
                Console.WriteLine("Thanks for using the Number To Words Program. Have a nice day.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
    public class IsNullOrWhiteSpaceException : Exception
    {
    }
    public class NumberToWordConverter
    {

       public String FormWordsInHundred(String hundredsValue)
        {
            String [] valuesUnderNineTeen  = new String [] { "","one" , "two", "three","four" ,"five", "six", "seven", "eight" , "nine", "ten", "eleven" , "twelve", "thirteen" ,"fourteen" , "fifteen", "sixteen" , "seventeen" , "eighteen" , "nineteen" };
            String[] tensPlaceValue = new String[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            String hundredsValueInWords = String.Empty;
            int placeValue = hundredsValue.Length;
            Int32 numberValue = 0;
            try
            {
                switch(placeValue)
                {
                    //ones place
                    case 1:
                        hundredsValueInWords = hundredsValueInWords + " " + valuesUnderNineTeen[Convert.ToInt32(hundredsValue)];
                        break;
                    //tens place
                    case 2:
                        numberValue = Convert.ToInt32(hundredsValue)/10;
                        if (numberValue > 1)
                            hundredsValueInWords = hundredsValueInWords + " " + tensPlaceValue[numberValue] + " " + valuesUnderNineTeen[Convert.ToInt32(hundredsValue) % 10];
                        else
                            goto case 1; 
                        break;
                    //hundreds place
                    case 3:
                        numberValue = Convert.ToInt32(hundredsValue) / 100;
                        if (Convert.ToInt32(hundredsValue) % 100 == 0)
                            hundredsValueInWords = valuesUnderNineTeen[numberValue] + " hundred";
                        else
                        {
                            hundredsValueInWords = valuesUnderNineTeen[numberValue] + " hundred and";
                            hundredsValue = (Convert.ToInt32(hundredsValue) % 100).ToString();
                            goto case 2;
                        }
                        break;
                }

            }
            catch(Exception ex)
            {
                return "";
            }
           return hundredsValueInWords;           
        }
    }
}
