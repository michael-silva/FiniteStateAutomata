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
                "baa!",
                "baaaaaaaaaaaaaaaaaa!"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "aaa!",
                "baaa",
                "baa!a",
                "abaa!"
            };
            
            for(int i = 0; i < sheeptalks.Length; i++)
            {
                for(int j = 0; j < matches.Length; j++)
                    Assert.True(sheeptalks[i].IsMatch(matches[j].ToCharArray()), matches[j]);
                    
                for(int j = 0; j < unmatches.Length; j++)
                    Assert.False(sheeptalks[i].IsMatch(unmatches[j].ToCharArray()), unmatches[j]);
            }
        }
        
        [Fact]
        public void Example2Test()
        {
            var sheeptalks = new [] {
                AutomataExamples.GetAutomata1().Closure(),
                AutomataExamples.GetAutomata2().Closure(),
                AutomataExamples.GetAutomata3().Closure()
            };
            
            var matches = new [] {
                "baa!",
                "baaaaaaaaaaaaaaaaaa!",
                "baa!baa!baa!baa!",
                "baa!baaaaaaaaaaaaaaaaaa!baa!"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "aaa!",
                "baaa",
                "baa!a",
                "abaa!",
                "baa!baa!baa!baa",
                "baa!bbaa!"
            };
            
            for(int i = 0; i < sheeptalks.Length; i++)
            {
                for(int j = 0; j < matches.Length; j++)
                    Assert.True(sheeptalks[i].IsMatch(matches[j].ToCharArray()), matches[j]);
                    
                for(int j = 0; j < unmatches.Length; j++)
                    Assert.False(sheeptalks[i].IsMatch(unmatches[j].ToCharArray()), unmatches[j]);
            }
        }
        
        [Fact]
        public void Example3Test()
        {
            var sheeptalks = new [] {
                AutomataExamples.GetAutomata2().Union(AutomataExamples.GetAutomata4())
            };
            
            var matches = new [] {
                "baa!",
                "baaaaaaaaaaaaaaaaaa!",
                "beeh",
                "beeeeeeeeeeeeeeeeeeh"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "aaa!",
                "baaa",
                "baa!a",
                "abaa!",
                "baa!baa!baa!baa",
                "baa!bbaa!"
            };
            
            for(int i = 0; i < sheeptalks.Length; i++)
            {
                for(int j = 0; j < matches.Length; j++)
                    Assert.True(sheeptalks[i].IsMatch(matches[j].ToCharArray()), matches[j]);
                    
                //for(int j = 0; j < unmatches.Length; j++)
                //    Assert.False(sheeptalks[i].IsMatch(unmatches[j].ToCharArray()), unmatches[j]);
            }
        }
        
        [Fact]
        public void Example4Test()
        {
            var moneycheck = AutomataExamples.GetAutomata4();
        }
    }
}