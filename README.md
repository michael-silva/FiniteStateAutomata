# Automata.Net
A implementation of Finite State Automatas Deterministic and  E-NonDeterministic in C#.

## Getting started
Simple example of use of deterministic automata to a sheeptalk
```
//Create a alphabet instance
var sheepabcd = new AutomataCharAlphabet("ba!");

//Instantiate a automata injecting alphabet
var sheeptalk = new DeterministicAutomata(sheepabcd);

//Define transitions
sheeptalk.State(1, null, null)
        .State(null, 2, null)
        .State(null, 3, null)
        .State(null, 3, 4)
        .State(null, null, null)
        .AcceptLast();

//And test automata
bool accept = sheeptalk.IsMatch("baaa!"); //Match consecutive string
bool reject = sheeptalk.IsMatch("bbaa!");
```

Examples of alphabets types.
```
var alphabetChar = new AutomataCharAlphabet("abcd");
var alphabet = new AutomataAlphabet("a", "b", "c");
var alphabetGroup = new AutomataGroupAlphabet()
            .Add("g1", new [] { "a", "b", "c" })
            .Add("g2", new [] { "d", "e", "f", "g" })
            .Add("g3", new[] { "h", "i" });
```

Examples of create automatas
```
var automata1 = new DeterministicAutomata(alphabet);
var automata2 = new NonDeterministicAutomata(alphabet);
```

Using the factory to create models of automata
```
var factory = new AutomataFactory("ba!");

var model = factory.NonDeterministic()
            .When('a').ToNext()
            .OnNext()
            .When('b').ToNext()
            .OnNext().Accept();

var automata = model.CreateAutomata();
```

Ways to define transitions
```
automata.State(1, null, null)
    .State(null, 2, null)
    .State(null, null, null)
    .AcceptLast();

automata.AddTransition('a', 0, 1); 
automata.AddTransition('b', 1, 2); 
automata.AcceptState(3);
```

Some test methods for automatas
```
automata.BeginMatch("abbaaaaa");
automata.EndMatch("aaaaabba");
automata.IsMatch("abba");
automata.SplitMatch("a,b,b,a", ',');
```

An advanced example
```
//Criação de alfabeto de caracteres agrupados
var alphabet = new AutomataGroupAlphabet()
                    .Add("single", new [] { "one" })
                    .Add("plural1", new [] { "two", "three", "four", "five", "six", "seven", "eight", "nine" })
                    .Add("plural2", new[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "sixteen", "seventeen", "eighteen", "nineteen" })
                    .Add("tens", new [] {"twenty", "thirty", "fourty", "fifity", "sixty", "seventy", "eighty", "ninety" })
                    .Add("cent")
                    .Add("cents")
                    .Add("dollar")
                    .Add("dollars");

//Factory para criação de automatas através de interface fluente
var factory = new AutomataFactory(alphabet);

//Definição de modelo de automata com estados e suas transições através de um Fluent Facade
var model = factory.NonDeterministic()
                .When("tens").To("q1").To("q2")
                .When("single").To("q2a")
                .When("plural2").To("q2")
                .When("plural1").To("q2")
            .On("q1")
                .When("single").To("q2")
                .When("plural1").To("q2")
            .On("q2")
                .When("dollars").To("q3").To("q6")
                .Epsilon().To("q5")
            .On("q2a")
                .When("dollar").To("q3").To("q6")
                .Epsilon().To("q5a")
            .On("q3")
                .Epsilon().To("q4")
                .When("tens").To("q4").To("q5")
                .When("single").To("q5a")
                .When("plural2").To("q5")
            .On("q4")
                .When("single").To("q5")
                .When("plural1").To("q5")
            .On("q5")
                .When("cents").To("q6")
            .On("q5a")
                .When("cent").To("q6")
            .On("q6").Accept();

//Getting a automata from model
var moneycheck =  model.CreateAutomata() as NonDeterministicAutomata;

//Testando o automata
var a1 = moneycheck.SplitMatch("sixty two dollars", ' ');
var a2 = moneycheck.SplitMatch("one cent", ' ');
var r1 = moneycheck.SplitMatch("sixty ten dollars", ' ');
var r2 = moneycheck.SplitMatch("one cents", ' ');

//Exibir resultado  
System.Console.WriteLine($"'sixty two dollars' is {a1}");
System.Console.WriteLine($"'one cent' is {a2}");
System.Console.WriteLine($"'sixty ten dollars' is {r1}");
System.Console.WriteLine($"'one cents' is {r2}");
```

### Next Steps
- Test Match with examples inside chapter 3
- Implement transducer
