
using astroHabitsCsharp.model;
using astroHabitsCsharp.util;

bool loopMenu = true;

while (loopMenu)
{
    Console.Clear();
    Console.WriteLine("1. NASA Task Load Index");
    Console.WriteLine("2. Modos de Órbita");
    Console.WriteLine("3. Ciclo Circadiano");
    Console.WriteLine("4. Sobre");
    Console.WriteLine("0. Sair");

    Console.Write("Sua escolha: ");

    // string entry = Console.ReadLine();
    string entry = "1";

    if (int.TryParse(entry, out int parsedEntry))
    {
        if (parsedEntry == 0)
        {
            Console.WriteLine("Tchau Tchau!");
            loopMenu = false;
            break;
        }
        if (parsedEntry == 1)
        {
            Console.WriteLine("1. NASA Task Load Index");
            Console.WriteLine("--------------------------------");

            Console.WriteLine("A carga de trabalho afeta diretamente aspectos físicos e mentais do indivíduo e\nconsequentemente seu desempenho. O instrumento NASA TLX adaptado auxilia no\nentendimento da carga de trabalho como um todo, pela abordagem dos aspectos gerais, como:\ndemanda mental, demanda física, demanda temporal, rendimento, esforço e nível de frustração.");
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Aqui podemos registrar e vizualizar tarefas concluidas e ver o nivel de cansaço do usuario");
            Console.WriteLine("--------------------------------");

            Console.WriteLine("1. Registrar tarefa concluida");
            Console.WriteLine("2. Vizualizar tarefas passadas");

            Console.WriteLine("Sua escolha: ");

            // entry = Console.ReadLine();
            entry = "1";

            if (int.TryParse(entry, out parsedEntry))
            {
                if (parsedEntry == 1)
                {
                    Console.WriteLine("Nome da tarefa: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Qual foi a demanda mental de 1 a 10: ");

                    string mentalDemand = Console.ReadLine();
                    int parsedMentalDemand = VerifyNumber.VerifyNumber1To10(mentalDemand);

                    Console.WriteLine("Qual foi a demanda fisica de 1 a 10: ");
                    string physicalDemand = Console.ReadLine();
                    int parsedPhysicalDemand = VerifyNumber.VerifyNumber1To10(physicalDemand);


                    Console.WriteLine(" de 1 a 10: ");
                    string temporalDemand = Console.ReadLine();
                    int parsedTemporalDemand = VerifyNumber.VerifyNumber1To10(temporalDemand);


                    Console.WriteLine("Quanto voce se daria de perfomace 1 a 10: ");
                    string performance = Console.ReadLine();
                    int parsedPerformance = VerifyNumber.VerifyNumber1To10(performance);


                    Console.WriteLine("O quanto voce se esforcou de 1 a 10: ");
                    string effort = Console.ReadLine();
                    int parsedEffort = VerifyNumber.VerifyNumber1To10(mentalDemand);


                    Console.WriteLine("Quao frustrante foi de 1 a 10: ");
                    string frustration = Console.ReadLine();
                    int parsedFrustration = VerifyNumber.VerifyNumber1To10(frustration);


                    new TaskLoadIndex(name, parsedMentalDemand, parsedPhysicalDemand, parsedTemporalDemand, parsedPerformance, parsedEffort, parsedFrustration);
                }
                else if(parsedEntry == 2)
                {
                    TaskLoadIndex.exibirTodosTLX();
                }
            }


            break;



        }
        if (parsedEntry == 2)
        {
            Console.WriteLine("2. Modos de Órbita");

        }
        if (parsedEntry == 3)
        {
            Console.WriteLine("3. Ciclo Circadiano");

        }
        if (parsedEntry == 4)
        {
            Console.WriteLine("Sobre");

        }
    }
    else
    {
        Console.WriteLine("Resposta invalida!");
    }

}