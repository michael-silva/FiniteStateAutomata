using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Facade
{
    public class StringAutomata: FluentAutomata<char>
    {
        public StringAutomata(IAutomata<char, char> automata)
             : base(automata)
        { }
        
        public bool IsMatch(string value)
        {
            return base.IsMatch(value.ToCharArray());
        }
        
        public Dictionary<int, int> Matches(string value)
        {
            return base.Matches(value.ToCharArray());
        }
    }   
}