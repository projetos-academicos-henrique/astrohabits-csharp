using System;
using System.Collections.Generic;
using System.Threading;
using astroHabitsCsharp.model;
using astroHabitsCsharp.util;
using astroHabitsCsharp.Exceptions; 

TaskLoadIndex.CarregarDados();
SleepEntry.CarregarDadosSono();

bool loopMenu = true;

while (loopMenu)
{
    Console.Clear();
    Console.WriteLine("--- AstroHabits ---");
    Console.WriteLine("Habitos Espaciais para a Vida na Terra");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("1. NASA Task Load Index");
    Console.WriteLine("2. Modos de Orbita (Fuga do Imediatismo)");
    Console.WriteLine("3. Ciclo Circadiano (Qualidade do Sono)");
    Console.WriteLine("4. Sobre");
    Console.WriteLine("0. Sair");

    Console.Write("Sua escolha: ");
    string entry = Console.ReadLine();

    
    if (int.TryParse(entry, out int parsedEntry))
    {
        switch (parsedEntry)
        {
            case 0:
                Console.WriteLine("Tchau Tchau!");
                loopMenu = false;
                break;

            case 1:
                MenuTLX();
                break;

            case 2:
                MenuOrbita();
                break;

            case 3:
                MenuCircadiano();
                break;

            case 4:
                MenuSobre();
                break;

            default:
                Console.WriteLine("Opcao invalida!");
                Console.ReadLine();
                break;
        }
    }
    else
    {
        Console.WriteLine("Resposta invalida! Digite um numero.");
        Console.ReadLine();
    }
}

void MenuTLX()
{
    Console.Clear();
    Console.WriteLine("1. NASA Task Load Index");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("A carga de trabalho afeta diretamente aspectos fisicos e mentais.");
    Console.WriteLine("O NASA TLX avalia 6 dimensoes para medir o quao exigente foi uma tarefa.");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("1. Registrar tarefa concluida");
    Console.WriteLine("2. Vizualizar tarefas passadas");
    Console.Write("Sua escolha: ");

    string entry = Console.ReadLine();
    if (int.TryParse(entry, out int subEntry))
    {
        if (subEntry == 1)
        {
            Console.Write("Nome da tarefa: ");
            string name = Console.ReadLine();
            try
            {
                int mentalDemand = TLXQuestion("Qual foi a demanda mental de 1 a 10: ");
                int physicalDemand = TLXQuestion("Qual foi a demanda fisica de 1 a 10: ");
                int temporalDemand = TLXQuestion("Demanda de tempo de 1 a 10: ");
                int performance = TLXQuestion("Quanto voce se daria de perfomace 1 a 10: ");
                int effort = TLXQuestion("O quanto voce se esforcou de 1 a 10: ");
                int frustration = TLXQuestion("Quao frustrante foi de 1 a 10: ");

                
                TaskLoadIndex newTask = new TaskLoadIndex(name, mentalDemand, physicalDemand, temporalDemand, performance, effort, frustration);

                TaskLoadIndex.SalvarDados();
                Console.WriteLine("Tarefa registrada com sucesso!");

                
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Resultado da Avaliacao:");
                newTask.PrintVisual();
            }
            catch (InvalidNumberRangeException ex) 
            {
                Console.WriteLine($"Erro de validacao: {ex.Message}");
            }
            catch (FormatException) 
            {
                Console.WriteLine("Erro: Voce deve digitar um numero valido.");
            }
        }
        else if (subEntry == 2)
        {
            Console.WriteLine("--------------------------------");
            TaskLoadIndex.ExibirTodosTLX();
        }
    }
    Console.WriteLine("Pressione Enter para continuar...");
    Console.ReadLine();
}




void MenuOrbita()
{
    Console.Clear();
    Console.WriteLine("2. Modos de Orbita - Fuga do Imediatismo");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("No espaco, uma mensagem demora para chegar.");
    Console.WriteLine("Isso parece ruim... mas talvez nao ter respostas na hora");
    Console.WriteLine("seja um exercicio de paciencia.");
    Console.WriteLine("");
    Console.WriteLine("Escolha uma orbita. Suas 'notificacoes' serao retidas pelo tempo de delay.");
    Console.WriteLine("Durante o delay, foque em si - as respostas virao quando chegar a hora.");
    Console.WriteLine("--------------------------------");

    
    OrbitData terra = OrbitData.Terra;
    OrbitData lua = OrbitData.Lua;
    OrbitData marte = OrbitData.Marte;

    Console.WriteLine($"1. {terra.GetInfo()}");
    Console.WriteLine($"2. {lua.GetInfo()}");
    Console.WriteLine($"3. {marte.GetInfo()}");

    Console.Write("Escolha sua orbita: ");
    string entry = Console.ReadLine();

    if (int.TryParse(entry, out int orbitChoice) && orbitChoice >= 1 && orbitChoice <= 3)
    {
        
        OrbitData orbitaSelecionada;
        switch (orbitChoice)
        {
            case 1: orbitaSelecionada = terra; break;
            case 2: orbitaSelecionada = lua; break;
            case 3: orbitaSelecionada = marte; break;
            default: orbitaSelecionada = terra; break;
        }

        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Orbita selecionada: {orbitaSelecionada.Name}");
        Console.WriteLine($"Distancia: {orbitaSelecionada.AltitudeKm:N0} km");
        Console.WriteLine($"Delay real: {orbitaSelecionada.GetDelayFormatted()}");
        Console.WriteLine("--------------------------------");

        if (orbitaSelecionada.DelaySeconds == 0)
        {
            
            Console.WriteLine("Comunicacao instantanea! Notificacoes chegam imediatamente.");
            Console.WriteLine("Lembre-se: nem sempre ter tudo na hora e o melhor para voce.");
            Console.WriteLine("Considere experimentar a orbita da Lua ou Marte!");
        }
        else
        {
            
            int numNotifications = orbitChoice == 2 ? 3 : 5; 
            List<Notification> notificacoesRetidas = new List<Notification>();

            Console.WriteLine($"Entrando em modo de foco... {numNotifications} notificacoes serao retidas.");
            Console.WriteLine($"Delay de comunicacao: ~{orbitaSelecionada.DelayRealMinutes} minutos (simulado em {orbitaSelecionada.DelaySeconds}s)");
            Console.WriteLine("");

            
            for (int i = 0; i < numNotifications; i++)
            {
                notificacoesRetidas.Add(Notification.GerarAleatoria());
            }

            
            Console.WriteLine("Aguardando sinal retornar...");
            for (int i = orbitaSelecionada.DelaySeconds; i > 0; i--)
            {
                
                Console.Write($"\rTempo restante: {i} segundos...   ");
                Thread.Sleep(1000); 
            }
            Console.WriteLine("\rSinal recebido!                    ");

            
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Suas notificacoes chegaram:");
            Console.WriteLine("");
            foreach (Notification notif in notificacoesRetidas)
            {
                notif.Entregar();
                Console.WriteLine(notif.ToString());
            }

            Console.WriteLine("");
            Console.WriteLine($"Voce ficou focado por {orbitaSelecionada.DelayRealMinutes} minutos (simulados)!");
            Console.WriteLine("Reflexao: As notificacoes esperaram, e o mundo nao acabou.");
            Console.WriteLine("Talvez voce nao precise de tudo na hora. Pratique a paciencia!");
        }
    }
    else
    {
        Console.WriteLine("Opcao invalida! Escolha 1, 2 ou 3.");
    }
    Console.WriteLine("Pressione Enter para continuar...");
    Console.ReadLine();
}




void MenuCircadiano()
{
    Console.Clear();
    Console.WriteLine("3. Ciclo Circadiano - Qualidade do Sono");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Na ISS(Estação Espacial Internacional), astronautas veem 16 nascer-do-sol por dia.");
    Console.WriteLine("Sem controle da luz, o ritmo circadiano colapsa.");
    Console.WriteLine("Tecnicas para reduzir luz azul sao essenciais no espaco.");
    Console.WriteLine("Voce pode usar as mesmas tecnicas aqui na Terra.");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("1. Analisar fase atual do dia");
    Console.WriteLine("2. Registrar sono");
    Console.WriteLine("3. Historico de sono");
    Console.Write("Sua escolha: ");

    string entry = Console.ReadLine();

    if (int.TryParse(entry, out int subEntry))
    {
        switch (subEntry)
        {
            case 1:
                
                Console.WriteLine("--------------------------------");
                string phase = SleepCalculator.GetCurrentPhase();
                string horaAtual = DateTime.Now.ToString("HH:mm");

                Console.WriteLine($"Horario atual: {horaAtual}");
                Console.WriteLine($"Fase do dia: {phase}");
                Console.WriteLine("--------------------------------");

                
                Console.WriteLine("Dicas para a fase atual:");
                string[] tips = SleepCalculator.GetTips(phase);
                foreach (string tip in tips)
                {
                    Console.WriteLine($"  - {tip}");
                }
                break;

            case 2:
                
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Informe seus dados de sono para receber recomendacoes.");
                Console.WriteLine("");

                
                try
                {
                    Console.Write("Horario que deseja dormir (HH:mm, ex: 23:00): ");
                    string sleepTimeInput = Console.ReadLine();

                    
                    
                    TimeSpan sleepTime = SleepCalculator.ParseTime(sleepTimeInput);

                    
                    TimeSpan cutoffTime = SleepCalculator.CalculateScreenCutoff(sleepTime);

                    Console.WriteLine($"Horario de sono: {sleepTime:hh\\:mm}");
                    Console.WriteLine($"Parar telas as: {cutoffTime:hh\\:mm} (2h antes de dormir)");

                    
                    TimeSpan tempoAteCutoff = SleepCalculator.TimeUntilCutoff(sleepTime);
                    if (tempoAteCutoff == TimeSpan.Zero)
                    {
                        Console.WriteLine("Ja passou do horario de parar as telas! Desligue logo!");
                    }
                    else
                    {
                        Console.WriteLine($"Faltam {tempoAteCutoff.Hours}h{tempoAteCutoff.Minutes:D2}min para parar as telas.");
                    }

                    
                    Console.WriteLine("");
                    Console.WriteLine("Como foi a qualidade do seu sono na ultima noite?");
                    int quality = TLXQuestion("Qualidade do sono (1 a 10): ");

                    string qualityLabel = SleepCalculator.GetSleepQualityLabel(quality);
                    Console.WriteLine($"Qualidade: {qualityLabel}");

                    
                    string currentPhase = SleepCalculator.GetCurrentPhase();

                    
                    SleepEntry newEntry = new SleepEntry(
                        sleepTime.ToString(@"hh\:mm"),
                        cutoffTime.ToString(@"hh\:mm"),
                        quality,
                        currentPhase
                    );

                    SleepEntry.SalvarDadosSono();
                    Console.WriteLine("Registro de sono salvo com sucesso!");
                    Console.WriteLine(newEntry.GetDetails()); 
                }
                catch (InvalidTimeException ex) 
                {
                    Console.WriteLine($"Erro de validacao: {ex.Message}");
                }
                catch (InvalidNumberRangeException ex) 
                {
                    Console.WriteLine($"Erro de validacao: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: Formato invalido. Use numeros validos.");
                }
                break;

            case 3:
                
                Console.WriteLine("--------------------------------");
                SleepEntry.ExibirTodosSleep();
                break;
        }
    }
    Console.WriteLine("Pressione Enter para continuar...");
    Console.ReadLine();
}




void MenuSobre()
{
    Console.Clear();
    Console.WriteLine("Sobre o AstroHabits");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Com a ideia de trazer solucoes 'espaciais' para o mundo comum,");
    Console.WriteLine("o AstroHabits traz 3 estrategias inspiradas na vida fora da Terra");
    Console.WriteLine("para ajudar voce a ter mais foco e uma vida mais leve.");
    Console.WriteLine("");
    Console.WriteLine("1. NASA TLX - Avalie sua carga de trabalho com o metodo da NASA");
    Console.WriteLine("2. Modos de Orbita - Pratique a paciencia com delay de comunicacao");
    Console.WriteLine("3. Ciclo Circadiano - Melhore seu sono reduzindo luz azul");
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Pressione Enter para continuar...");
    Console.ReadLine();
}







int TLXQuestion(string question)
{
    Console.Write(question);
    string entry = Console.ReadLine();
    int parsedEntry = int.Parse(entry); 

    
    
    VerifyNumber.VerifyNumberRange(parsedEntry, 1, 10);

    return parsedEntry;
}