using System.Collections.Generic;

namespace StringCalculatorKata
{
    public interface IStringCalculatorArgumentParser
    {
        IEnumerable<int> ParseArguments(string arguments);
    }
}