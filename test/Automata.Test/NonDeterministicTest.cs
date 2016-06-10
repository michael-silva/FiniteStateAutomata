using Automata.Console;
using Xunit;

namespace Automata.Test
{
    /*************
    * preparar injeção de optimizer 
    * implementar nondeterministic
    * implementar regex
    * testar cast
    ************/
    public class NonDeterministicTest
    {
        [Fact]
        public void Example1Test()
        {
            var sheeptalks = new [] {
                Samples.Sheeptalk1(),
                Samples.Sheeptalk2(),
                Samples.Sheeptalk3()
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
                Samples.StuttererSeeptalk(),
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
                Samples.Sheeptalk1().Closure(),
                Samples.Sheeptalk2().Closure(),
                Samples.Sheeptalk3().Closure()
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
                Samples.Sheeptalk2().Union(Samples.StuttererSeeptalk())
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
                Samples.Sheeptalk2().Intersection(Samples.StuttererSeeptalk())
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
                Samples.Sheeptalk2().Concat(Samples.StuttererSeeptalk())
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
            var sheeptalks = new [] {
                Samples.Sheeptalk2().Concat(Samples.StuttererSeeptalk())
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
        public void Example8Test()
        {
            var money = Samples.Money();
            
            var matches = new [] { 
                "twenty one dollars one cent",
                "eleven dollars five cents",
                "one dollar ten cents",
                "five dollars",
                "ninety cents"
            };
            
            var unmatches = new [] { 
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
                
            for(int j = 0; j < matches.Length; j++)
                Assert.False(money.AnyMatch(unmatches[j].Split(' ')), unmatches[j]);
            
            for(int j = 0; j < matches.Length; j++)
            {
                int n = new System.Random().Next(10, 20);
                string temp = "";
                for(int z = 0; z < n; z++)
                    temp += ((z % 2 == 0 ? "teste " : "") + matches[j] + (z > n / 2 ? " teste" : "")) + " ";
                
                Assert.True(money.Matches(temp.Split(' ')).Count == n, $"{temp} {money.Matches(temp.Split(' ')).Count} <> {n}");
            }
        }
    }
}