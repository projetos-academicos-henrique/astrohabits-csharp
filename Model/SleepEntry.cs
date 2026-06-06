using System;
using System.Collections.Generic;

namespace astroHabitsCsharp.model
{
    
    
    public partial class SleepEntry : Entry
    {
        
        
        public string DesiredSleepTime { get; set; }   
        public string ScreenCutoffTime { get; set; }   
        public int SleepQuality { get; set; }           
        public string CurrentPhase { get; set; }        

        
        private static List<SleepEntry> listOfSleep = new List<SleepEntry>();

        
        public SleepEntry() { }

        
        public SleepEntry(string desiredSleepTime, string screenCutoffTime, int sleepQuality, string currentPhase)
        {
            this.DesiredSleepTime = desiredSleepTime;
            this.ScreenCutoffTime = screenCutoffTime;
            this.SleepQuality = sleepQuality;
            this.CurrentPhase = currentPhase;

            listOfSleep.Add(this);
        }

        
        
        
        public override string GetDetails()
        {
            return $"[{CreatedDate}] Sono: {DesiredSleepTime} | Qualidade: {SleepQuality}/10 | Fase: {CurrentPhase}";
        }

        

        
        
        
        public static void ExibirTodosSleep()
        {
            if (listOfSleep.Count == 0)
            {
                Console.WriteLine("Nenhum registro de sono encontrado.");
                return;
            }

            foreach (SleepEntry item in listOfSleep)
            {
                item.Print();
                Console.WriteLine(item.GetDetails()); 
                Console.WriteLine("-------------------------");
            }
        }

        
        
        
        public void Print()
        {
            Console.WriteLine($"Horario de Sono: {DesiredSleepTime}");
            Console.WriteLine($"Parar Telas: {ScreenCutoffTime}");
            Console.WriteLine($"Qualidade: {SleepQuality}/10");
            Console.WriteLine($"Fase do Dia: {CurrentPhase}");
        }
    }
}
