using System;
using System.Collections.Generic;
using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Facade;
using Automata.Core.Interfaces;

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
                    .State(null, 2, 3)
                    .State(null, null, null)
                    .AcceptLast();
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata2()
		{
            //Instanciação do automata com string que define um alphabeto de chars
            var sheeptalk = new DeterministicAutomata("ba!");
            
            //Adicionando transições
            sheeptalk.AddTransition('b', 0, 1); 
            sheeptalk.AddTransition('a', 1, 2);
            sheeptalk.AddTransition('a', 2, 2);
            sheeptalk.AddTransition('!', 2, 3);
            
            //Define o 4º estado como aceito
            sheeptalk.AcceptState(3);
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata3()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Factory para criação de automatas através de interface fluente 
            var sheepfactory = new AutomataFactory(sheepabcd);
            
            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
            var sheepmodel = sheepfactory.Deterministic()
                                .When("b").ToNext()
                                .OnNext()
                                .When("a").ToNext()
                                .OnNext()
                                .When("a").Repeat()
                                .When("!").ToNext()
                                .OnNext().Accept();
                                
            //Getting a automata from model
            return (DeterministicAutomata)sheepmodel.CreateAutomata();
        }
    }
}