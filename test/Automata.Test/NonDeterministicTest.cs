using System.Collections.Generic;
using Automata.Console.Samples;
using Xunit;

namespace Automata.Test
{
    public class NonDeterministicTest
    {
        [Fact]
        public void Example1Test()
        {
            var sheeptalks = new [] {
                NonDeterministics.Sheeptalk1(),
                NonDeterministics.Sheeptalk2()
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
                NonDeterministics.StuttererSeeptalk(),
            };
            
            var matches = new [] {
                "baa!",
                "baabaa!",
                "baaaaaaaaaaaaaaaaaabaa!",
            };
            
            var unmatches = new [] {
                "baaaaaaaaaaaaaaaaaabaaaaa!",
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
        public void Example3Test()
        {
            var sheeptalks = new [] {
                NonDeterministics.Sheeptalk1().Closure(),
                NonDeterministics.Sheeptalk2().Closure()
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
        public void Example4Test()
        {
            var sheeptalks = new [] {
                NonDeterministics.StuttererSeeptalk().Union(NonDeterministics.Sheeptalk1()),
                NonDeterministics.Sheeptalk2().Union(NonDeterministics.StuttererSeeptalk())
            };
            
            var matches = new [] {
                "baa!",
                "baaaaaaaaaaaaaaaaaa!",
                "baabaa!",
                "baaaaaaaaaaaaaaaaaabaa!"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "baaaba!",
                "baaabaaaa!",
                "babaaa!",
                "bbaa!",
                "baaa"
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
        public void Example5Test()
        {
            var sheeptalks = new [] {
                NonDeterministics.Sheeptalk2().Intersection(NonDeterministics.StuttererSeeptalk())
            };
            
            var matches = new [] {
                "baa!"
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "baaaba!",
                "baaabaaaa",
                "babaaa!",
                "bbaa!",
                "baaa",
                "baaaaaaaaaaaaaaaaaa!",
                "baabaa!",
                "baaaaaaaaaaaaaaaaaabaabaaaaa!"
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
        public void Example6Test()
        {
            var sheeptalks = new [] {
                NonDeterministics.Sheeptalk2().Concat(NonDeterministics.StuttererSeeptalk())
            };
            
            var matches = new [] {
                "baa!baaaaaaaaaaaaaaaaaabaa!",
                "baaaaaaaaaaaaaaaaaa!baabaa!",
            };
            
            var unmatches = new [] {
                "ba!",
                "bbaa!",
                "baaaba!",
                "baaabaaaa",
                "babaaa!",
                "bbaa!",
                "baaa",
                "baabaa!",
                "baaaaaaaaaaaaaaaaaabaabaaaaa!"
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
        public void Example7Test()
        {
            var money = NonDeterministics.Money();
            
            var tempMatches = new List<string>() {
                "twenty one dollars",
                "sixty dollars",
                "seventeen dollars",
                "nine dollars",
                "one dollar", 
                "seventy six cents",
                "sixty cents",
                "seventeen cents",
                "nine cents",
                "one cent"
            };

            int length = tempMatches.Count;
            for(int i = 0; i < length / 2; i++)
                for(int j = length / 2; j < length; j++)
                    tempMatches.Add(tempMatches[i] + " " + tempMatches[j]);

            var matches = tempMatches.ToArray();
            var unmatches = new [] {
                "twenty one dollar one cents",
                "eleven one dollars five cent",
                "ninety",
                "one dollars",
                "five dollar",
                "ninety seven cent"
            };
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.IsMatch(matches[j].Split(' ')), "Test Ismatch: " + matches[j]);
                
            for(int j = 0; j < unmatches.Length; j++)
                Assert.False(money.IsMatch(unmatches[j].Split(' ')), "Test Ismatch: " + unmatches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.SplitMatch(matches[j], ' '), "Test Splitmatch: " + matches[j]);
                
            for(int j = 0; j < unmatches.Length; j++)
                Assert.False(money.SplitMatch(unmatches[j], ' '), "Test Splitmatch: " + unmatches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.BeginMatch((matches[j] + (j % 2 == 0 ? " teste" : "")).Split(' ')), "Test Beginmatch: " + matches[j] + (j % 2 == 0 ? " teste" : ""));
                
            for(int j = 0; j < matches.Length; j++)
                Assert.False(money.BeginMatch(("teste " + matches[j]).Split(' ')),  "Test Beginmatch: teste " + matches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.EndMatch(((j % 2 == 0 ? "teste " : "") + matches[j]).Split(' ')), "Test Endmatch: " + matches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.False(money.EndMatch((matches[j] + " teste").Split(' ')), "Test Endmatch: " + matches[j] + " teste");
                
            for(int j = 0; j < matches.Length; j++)
            {
                string temp = ((j % 2 == 0 ? "teste " : "") + matches[j] + (j > matches.Length / 2 ? " teste" : ""));
                Assert.True(money.AnyMatch(temp.Split(' ')), "Test Anymatch: " + temp);
            }
                
            for(int j = 2; j < unmatches.Length; j++)
                Assert.False(money.AnyMatch(unmatches[j].Split(' ')), "Test Anymatch: " + unmatches[j]);
            
            for(int j = 0; j < matches.Length; j++)
            {
                int n = new System.Random().Next(10, 20);
                string temp = "";
                for(int z = 0; z < n; z++)
                    temp += ((z % 2 == 0 ? "teste " : "") + matches[j] + (z > n / 2 ? " teste" : "")) + " ";
                var results = money.Matches(temp.Split(' '));
                Assert.True(results.Count == n, $"{temp} {results.Count} <> {n}");
            }
        }
        
        [Fact]
        public void Example8Test()
        {
            var money = NonDeterministics.Money().ToDeterministic();
            
            var tempMatches = new List<string>() {
                "twenty one dollars",
                "sixty dollars",
                "seventeen dollars",
                "nine dollars",
                "one dollar", 
                "sixty cents",
                "seventy six cents",
                "seventeen cents",
                "nine cents",
                "one cent"
            };

            int length = tempMatches.Count;
            for(int i = 0; i < length / 2; i++)
                for(int j = length / 2; j < length; j++)
                    tempMatches.Add(tempMatches[i] + " " + tempMatches[j]);

            var matches = tempMatches.ToArray();
            var unmatches = new [] {
                "ninety",
                "twenty one dollar one cents",
                "eleven one dollars five cent",
                "one dollars ten",
                "five dollar",
                "ninety seven cent"
            };
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.IsMatch(matches[j].Split(' ')), matches[j]);
                
            for(int j = 0; j < unmatches.Length; j++)
                Assert.False(money.IsMatch(unmatches[j].Split(' ')), unmatches[j]);        
        }
    }
}