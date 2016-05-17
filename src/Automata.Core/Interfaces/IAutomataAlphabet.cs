using System;

namespace Automata.Core.Interfaces
{
    public interface IAutomataAlphabet
    {
        int Length { get; }
        
        int IndexOf(IComparable key);

        int IndexOfValue(IComparable value);
    }
}
