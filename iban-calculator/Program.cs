using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace iban_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Muunna suomalainen tili IBAN muotoon");
            Console.WriteLine("Anna tilinumero");
            string accNumber = Console.ReadLine();
            string fi = "151800";

            int zeroStart = checkFnumber(accNumber);            //check when 0 start's
            string addZ = addZero(accNumber, zeroStart);        //add 0++
            decimal remainder = calculate(addZ, fi);            //remainder
            string extraZ = "";
            if (remainder < 10)
            {
                extraZ = "0";
            }

            //Print
            Console.WriteLine("iban= FI{0}{1} {2}", remainder, extraZ, Regex.Replace(addZ, ".{4}", "$0 "));
            Console.ReadLine();
        }

        static public int checkFnumber(string accNumber)
        {

            accNumber = accNumber.Replace("-", String.Empty);
            int checkF = int.Parse(accNumber.Substring(0, 1));
            if (checkF == 5 || checkF == 4)
            {
                int zeroStart = 7;
                return zeroStart;
            }
            else
            {
                int zeroStart = 6;
                return zeroStart;
            }
        }

        static public string addZero(string accNumber, int zeroStart)
        {
            accNumber = accNumber.Replace("-", String.Empty);
            int length = accNumber.Length;
            string zeroNum = "";
            if (length <= 14)
            {
                for (int i = length + 1; i <= 14; i++)
                {
                    zeroNum += "0";
                }
                accNumber = accNumber.Insert(zeroStart, zeroNum);

                string addZ = accNumber;
                return addZ;
            }
            else
            {
                string addZ = "liikaa numeroita";               ///korjaa
                return addZ;
            }
        }

        static public decimal calculate(string addZ, string fi)
        {
            addZ = addZ.Replace("-", String.Empty);
            string sum = (addZ + fi);
            decimal convertSum = decimal.Parse(sum);
            decimal remainder = 98 - (convertSum % 97);
            return remainder;
        }
    }
}

