using System;
using Automata.Console;
using Xunit;

namespace Automata.Test
{
    public class ExamplesTest
    {
        [Fact]
        public void Example1Test()
        {
            var sheeptalks = new [] {
                AutomataExamples.GetAutomata1(),
                AutomataExamples.GetAutomata2(),
                AutomataExamples.GetAutomata3()
            };
            
            var matches = new [] {
                "baaa!",
                "baaaaaaaaaaaaaaaaaa!"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "aaa!",
                "baaa"
            };
            
            for(int i = 0; i < sheeptalks.Length; i++)
            {
                for(int j = 0; j < matches.Length; j++)
                    Assert.True(sheeptalks[i].IsMatch(matches[j]), matches[j]);
                    
                for(int j = 0; j < matches.Length; j++)
                    Assert.False(sheeptalks[i].IsMatch(unmatches[j]), unmatches[j]);
            }
        }
        
        [Fact]
        public void Example2Test()
        {
            
        }
        
        [Fact]
        public void Example3Test()
        {
            var sheeptalk3 = AutomataExamples.GetAutomata3();

        }
        
        [Fact]
        public void Example4Test()
        {
            var moneycheck = AutomataExamples.GetAutomata4();

        }
    }
}