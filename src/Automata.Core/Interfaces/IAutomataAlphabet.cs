using System.Collections.Generic;

namespace Automata.Core.Interfaces
{
    public interface IAutomataAlphabet
    {
        int Length { get; }
        
        bool Equals(IAutomataAlphabet key);
        
        int IndexOf(object key);

        int IndexOfValue(object value);

        List<string> ValuesFrom(string value);
    }
}
