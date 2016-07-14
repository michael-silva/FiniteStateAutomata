# Automata.Net
A implementation of Finite State Automatas Deterministic and  E-NonDeterministic in C#.

## Getting started
Create a alphabet instance
```var sheepabcd = new AutomataCharAlphabet("ba!");```
Instantiate a automata injecting alphabet
```var sheeptalk = new DeterministicAutomata(sheepabcd);```

Define transitions
```
sheeptalk.State(1, null, null)
        .State(null, 2, null)
        .State(null, 3, null)
        .State(null, 3, 4)
        .State(null, null, null)
        .AcceptLast();
```

And test automata
```
bool accept = sheeptalk.IsMatch("baaa!"); //Match consecutive string
bool reject = sheeptalk.IsMatch("bbaa!");
```

Other types of alphabets
```
var alphabetChar = new AutomataCharAlphabet("abcd");
var alphabet = new AutomataAlphabet("a", "b", "c");
var alphabetGroup = new AutomataGroupAlphabet()
            .Add("g1", new [] { "a", "b", "c" })
            .Add("g2", new [] { "d", "e", "f", "g" })
            .Add("g3", new[] { "h", "i" });
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
Using the factory
//Factory para criação de automatas através de interface fluente e string que define um alfabeto de chars
var sheepfactory = new AutomataFactory("ba!");

//Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
var sheepmodel = sheepfactory.Deterministic()
            .When('b').ToNext()
            .OnNext()
            .When('a').ToNext()
            .OnNext()
            .When('a').ToNext()
            .OnNext()
            .When('a').Repeat()
            .When('!').ToNext()
            .OnNext().Accept();

//Getting a automata from model
return sheepmodel.CreateAutomata() as DeterministicAutomata;

Test Methods

Advanced Example
//Método de exemplo para criação de automata
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
var moneycheck = factory.Deterministic()
            .When("single").To("q2a")
            .When("plural1").To("q2")
            .When("plural2").To("q2")
            .When("tens").To("q1")
        .On("q1")
            .When("cents").To("q7")
            .When("dollars").To("q4")
            .When("single").To("q2")
            .When("plural1").To("q2")
        .On("q2a")
            .When("cent").To("q7")
            .When("dollar").To("q4")
        .On("q2")
            .When("cents").To("q7")
            .When("dollars").To("q4")
        .On("q4").Accept()
            .When("single").To("q6a")
            .When("plural1").To("q6")
            .When("plural2").To("q6")
            .When("tens").To("q5")
        .On("q5").Accept()
            .When("cents").To("q7")
            .When("single").To("q6")
            .When("plural1").To("q6")
        .On("q6a")
            .When("cent").To("q7")
        .On("q6")
            .When("cents").To("q7")
        .On("q7").Accept();

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

# Next Steps
comment the code
Test Match with examples inside chapter 3
Implement transducer
