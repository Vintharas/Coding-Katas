/*
 * String Calculator Kata for improving TDD (from osherove's blog http://osherove.com/tdd-kata-1/)
 * 
 * 
 * 
 *  Before you start:
 *
 *     - Try not to read ahead.
 *     - Do one task at a time. The trick is to learn to work incrementally.
 *     - Make sure you only test for correct inputs. there is no need to test for invalid inputs for this kata
 *
 *  String Calculator Kata 
 *
 *  1.  Create a simple String calculator with a method int Add(string numbers)
 *      a.  The method can take 0, 1 or 2 numbers, and will return their sum (for an empty string it will return 0) for example “” or “1” or “1,2”
 *      b.  Start with the simplest test case of an empty string and move to 1 and two numbers
 *      c.  Remember to solve things as simply as possible so that you force yourself to write tests you did not think about
 *      d.  Remember to refactor after each passing test
 *  2.  Allow the Add method to handle an unknown amount of numbers
 *  3.  Allow the Add method to handle new lines between numbers (instead of commas).
 *      a.  the following input is ok:  “1\n2,3”  (will equal 6)
 *      b.  the following input is NOT ok:  “1,\n” (not need to prove it - just clarifying)
 *  4.  Support different delimiters
 *      a.  to change a delimiter, the beginning of the string will contain a separate line that looks like this:   “//[delimiter]\n[numbers…]” for example “//;\n1;2” should return three where the default delimiter is ‘;’ .
 *      b.  the first line is optional. all existing scenarios should still be supported
 *  5.  Calling Add with a negative number will throw an exception “negatives not allowed” - and the negative that was passed.if there are multiple negatives, show all of them in the exception message
 *  
 *  -----------------------------------------------------------------------------------------------------
 *  stop here if you are a beginner. Continue if you can finish the steps so far in less than 30 minutes.
 *  -----------------------------------------------------------------------------------------------------
 *  
 *  6.  Numbers bigger than 1000 should be ignored, so adding 2 + 1001  = 2
 *  7.  Delimiters can be of any length with the following format:  “//[delimiter]\n” for example: “//[***]\n1***2***3” should return 6
 *  8.  Allow multiple delimiters like this:  “//[delim1][delim2]\n” for example “//[*][%]\n1*2%3” should return 6.
 *  9.  make sure you can also handle multiple delimiters with length longer than one char
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        private const string NEGATIVE_NUMBERS_MESSAGE = "Negative numbers not allowed: {0}";
        private const string CUSTOM_DELIMITER_PATTERN = "//[{0}]\n";
        private const string RIGHT_BRACKET = "//[";
        private const string LEFT_BRACKET = "]\n";

        public int Add(string numbers)
        {
            var customDelimiter = "";
            if (numbers == "")
                return 0;
            if (numbers.HaveCustomDelimiter())
            {
                customDelimiter = ExtractDelimiter(numbers);
                numbers = numbers.Replace(string.Format(CUSTOM_DELIMITER_PATTERN, customDelimiter), "");
            }
            else if (numbers.ContainsOnlyOneNumber())
                return int.Parse(numbers);
            return Add(numbers.Split(new string[]{",", "\n", customDelimiter}, options: StringSplitOptions.None));
        }

        private string ExtractDelimiter(string numbers)
        {
            var match = Regex.Match(numbers, "^//\\[.*\\]\n");
            return match.Value.Replace(RIGHT_BRACKET, "").Replace(LEFT_BRACKET, "");
        }

        private int Add(IEnumerable<string> numbers)
        {
            if (numbers.HaveNegativeNumbers())
                throw new NegativeNumbersNotAllowedException(negativeNumbers: ExtractNegativeNumbers(numbers));
            return numbers.Select(int.Parse).Select(RemoveNumbersBiggerThanAThousand).Sum();
        }

        private int RemoveNumbersBiggerThanAThousand(int operand)
        {
            if (operand >= 1000)
                return 0;
            return operand;
        }

        private IEnumerable<int> ExtractNegativeNumbers(IEnumerable<string> numbers)
        {
            return numbers.Select(n => int.Parse(n)).Where(n => n < 0).ToList();
        }
    }

    public static class StringExtensionMethods
    {
        public static bool ContainsOnlyOneNumber(this string numbers)
        {
            return !numbers.Contains(",") && !numbers.Contains("\n");
        }

        public static bool HaveCustomDelimiter(this string numbers)
        {
            return Regex.Match(numbers, "^//\\[.*\\]\n").Success;
        }

        public static bool HaveNegativeNumbers(this IEnumerable<string> numbers)
        {
            return numbers.Any(n => int.Parse(n) < 0);
        }
    }
}
