using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Facade;

namespace Automata.Console
{
    public static class AutomataExamples
    {
        public static DeterministicAutomata GetAutomata1()
		{
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Instanciação do automata com um alfabeto
            var sheeptalk = new DeterministicAutomata(sheepabcd);
            
            //Definição da tabela de transições
            sheeptalk.State(1, null, null)
                    .State(null, 2, null)
                    .State(null, 3, null)
                    .State(null, 3, 4)
                    .State(null, null, null)
                    .AcceptLast();
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata2()
		{
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Instanciação do automata com um alfabeto
            var sheeptalk = new DeterministicAutomata(sheepabcd);
            
            //Adicionando transições
            sheeptalk.AddTransition('b', 0, 1); 
            sheeptalk.AddTransition('a', 1, 2); 
            sheeptalk.AddTransition('a', 2, 3);
            sheeptalk.AddTransition('a', 3, 3);
            sheeptalk.AddTransition('!', 3, 4);
            
            //Define o 4º estado como aceito
            sheeptalk.AcceptState(4);
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata3()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet();
            
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
        }
        
		public static DeterministicAutomata GetAutomata4()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet();
            
            //Factory para criação de automatas através de interface fluente e string que define um alfabeto de chars
            var sheepfactory = new AutomataFactory("beh");
            
            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
            var sheepmodel = sheepfactory.Deterministic()
                                .When('b').ToNext()
                                .OnNext()
                                .When('e').ToNext()
                                .OnNext()
                                .When('e').ToNext()
                                .OnNext()
                                .When('e').Repeat()
                                .When('h').ToNext()
                                .OnNext().Accept();
                                
            //Getting a automata from model
            return sheepmodel.CreateAutomata() as DeterministicAutomata;
        }

        public static DeterministicAutomata GetAutomata5()
        {
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
            var model = factory.Deterministic()
                            .When("single").To("q2a")
                            .When("plural1").To("q2")
                            .When("plural2").To("q2")
                            .When("tens").To("q1")
                        .On("q1").Accept()
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
                            .When("single").To("q6")
                            .When("plural1").To("q6")
                        .On("q6a")
                            .When("cent").To("q7")
                        .On("q6")
                            .When("cents").To("q7")
                        .On("q7").Accept();

            //Getting a automata from model
            return model.CreateAutomata();
        }
    }
}