using System;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.FiniteState
{
    public class RegularExpressionAutomata : IAutomata
    {
        public RegularExpressionAutomata(string pattern, string modifier) 
        {
            throw new NotImplementedException();
        }

        public bool Acceptance
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAutomataAlphabet Alphabet
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Empty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Totality
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AnyMatch(params char[] values)
        {
            throw new NotImplementedException();
        }

        public bool AnyMatch(params string[] values)
        {
            throw new NotImplementedException();
        }

        public IAutomata Clone()
        {
            throw new NotImplementedException();
        }

        public bool EndMatch(params char[] values)
        {
            throw new NotImplementedException();
        }

        public bool EndMatch(params string[] values)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IAutomata value)
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(params char[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsMatch(params string[] values)
        {
            throw new NotImplementedException();
        }

        public List<int[]> Matches(params char[] values)
        {
            throw new NotImplementedException();
        }

        public List<int[]> Matches(params string[] values)
        {
            throw new NotImplementedException();
        }

        public bool SubsetOf(IAutomata automata)
        {
            throw new NotImplementedException();
        }
    }
}