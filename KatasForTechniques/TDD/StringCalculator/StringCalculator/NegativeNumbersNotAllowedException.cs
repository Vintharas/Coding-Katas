using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StringCalculatorKata
{

    [Serializable]
    public class NegativeNumbersNotAllowedException : Exception
    {
        private const string NEGATIVE_NUMBERS_MESSAGE = "Negative numbers not allowed: {0}";

        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NegativeNumbersNotAllowedException()
        {
        }

        public NegativeNumbersNotAllowedException(string message) : base(message)
        {
        }

        public NegativeNumbersNotAllowedException(IEnumerable<int> negativeNumbers)
            : this(string.Format(NEGATIVE_NUMBERS_MESSAGE, string.Join(",", negativeNumbers)))
        {
            
        }

        public NegativeNumbersNotAllowedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NegativeNumbersNotAllowedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }


}