using System.Collections.Generic;
using Automata.Console.Samples;
using Xunit;

namespace Automata.Test
{
    public class DeterministicTest
    {
        [Fact]
        public void Example1Test()
        {
            var sheeptalks = new [] {
                Deterministics.Sheeptalk1(),
                Deterministics.Sheeptalk2(),
                Deterministics.Sheeptalk3()
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
                Deterministics.StuttererSeeptalk(),
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
                Deterministics.Sheeptalk1().Closure(),
                Deterministics.Sheeptalk2().Closure(),
                Deterministics.Sheeptalk3().Closure()
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
                Deterministics.Sheeptalk2().Union(Deterministics.StuttererSeeptalk())
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
                Deterministics.Sheeptalk2().Intersection(Deterministics.StuttererSeeptalk())
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
                Deterministics.Sheeptalk2().Concat(Deterministics.StuttererSeeptalk())
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
            var money = Deterministics.Money();
            
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
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.SplitMatch(matches[j], ' '), matches[j]);
                
            for(int j = 0; j < unmatches.Length; j++)
                Assert.False(money.SplitMatch(unmatches[j], ' '), unmatches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.BeginMatch((matches[j] + (j % 2 == 0 ? " teste" : "")).Split(' ')), matches[j] + (j % 2 == 0 ? " teste" : ""));
                
            for(int j = 0; j < matches.Length; j++)
                Assert.False(money.BeginMatch(("teste " + matches[j]).Split(' ')),  "teste " + matches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.True(money.EndMatch(((j % 2 == 0 ? "teste " : "") + matches[j]).Split(' ')), matches[j]);
                
            for(int j = 0; j < matches.Length; j++)
                Assert.False(money.EndMatch((matches[j] + " teste").Split(' ')), matches[j] + " teste");
                
            for(int j = 0; j < matches.Length; j++)
            {
                string temp = ((j % 2 == 0 ? "teste " : "") + matches[j] + (j > matches.Length / 2 ? " teste" : ""));
                Assert.True(money.AnyMatch(temp.Split(' ')), temp);
            }
                
            for(int j = 0; j < unmatches.Length; j++)
                Assert.False(money.AnyMatch(unmatches[j].Split(' ')), unmatches[j]);
            
            for(int j = 0; j < matches.Length; j++)
            {
                int n = new System.Random().Next(10, 20);
                string temp = "";
                for(int z = 0; z < n; z++)
                    temp += ((z % 2 == 0 ? "teste " : "") + matches[j] + (z > n / 2 ? " teste" : "")) + " ";
                
                var results = money.Matches(temp.Split(' '));
                Assert.True(results.Count == n, $"{temp} {results.Count} <> {n}");
            }

            //for(int j = 0; j < matches.Length; j++)
            //    Assert.True(money.Match(matches[j].Replace(" ", "")), matches[j]);
        }
    }
}