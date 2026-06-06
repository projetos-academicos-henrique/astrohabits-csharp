using System;
using System.Collections.Generic;

namespace astroHabitsCsharp.model
{
    
    
    public partial class TaskLoadIndex : Entry
    {
        
        public string Name { get; set; }
        public int MentalDemand { get; set; }
        public int PhysicalDemand { get; set; }
        public int TemporalDemand { get; set; }
        public int Performance { get; set; }
        public int Effort { get; set; }
        public int Frustration { get; set; }

        
        private static List<TaskLoadIndex> listOfTLX = new List<TaskLoadIndex>();

        
        public TaskLoadIndex() { }

        
        public TaskLoadIndex(string name, int mentalDemand, int physicalDemand, int temporalDemand, int performance, int effort, int frustration)
        {
            this.Name = name;
            this.MentalDemand = mentalDemand;
            this.PhysicalDemand = physicalDemand;
            this.TemporalDemand = temporalDemand;
            this.Performance = performance;
            this.Effort = effort;
            this.Frustration = frustration;

            listOfTLX.Add(this);
        }

        
        
        public override string GetDetails()
        {
            double score = CalculateWeightedScore();
            string classification = GetClassification();
            return $"[{CreatedDate}] Tarefa: {Name} | Score: {score:F1}/10 ({classification})";
        }

        

        
        
        
        
        
        public double CalculateWeightedScore()
        {
            
            return (MentalDemand + PhysicalDemand + TemporalDemand + (10 - Performance) + Effort + Frustration) / 6.0;
        }

        
        
        
        public string GetClassification()
        {
            double score = CalculateWeightedScore();

            if (score <= 3.0)
                return "Baixa";
            else if (score <= 5.0)
                return "Moderada";
            else if (score <= 7.5)
                return "Alta";
            else
                return "Critica";
        }

        

        
        
        
        public static void ExibirTodosTLX()
        {
            if (listOfTLX.Count == 0)
            {
                Console.WriteLine("Nenhum registro encontrado.");
                return;
            }

            foreach (TaskLoadIndex item in listOfTLX)
            {
                item.PrintVisual();
                Console.WriteLine("-------------------------");
            }
        }

        
        
        
        
        public void PrintVisual()
        {
            Console.WriteLine($"Tarefa: {Name}");

            
            
            Console.WriteLine($"  Demanda Mental:   {new string('#', MentalDemand),-10} ({MentalDemand}/10)");
            Console.WriteLine($"  Demanda Fisica:   {new string('#', PhysicalDemand),-10} ({PhysicalDemand}/10)");
            Console.WriteLine($"  Demanda Temporal: {new string('#', TemporalDemand),-10} ({TemporalDemand}/10)");
            Console.WriteLine($"  Performance:      {new string('#', Performance),-10} ({Performance}/10)");
            Console.WriteLine($"  Esforco:          {new string('#', Effort),-10} ({Effort}/10)");
            Console.WriteLine($"  Frustracao:       {new string('#', Frustration),-10} ({Frustration}/10)");

            
            double score = CalculateWeightedScore();
            string classification = GetClassification();
            Console.WriteLine($"  Score: {score:F1}/10 - Classificacao: {classification}");

            
            Console.WriteLine(GetDetails());
        }

        
        
        
        public void Print()
        {
            Console.WriteLine($"Nome: {Name}");
            Console.WriteLine($"Demanda Mental: {MentalDemand}");
            Console.WriteLine($"Demanda Fisica: {PhysicalDemand}");
            Console.WriteLine($"Demanda Temporal: {TemporalDemand}");
            Console.WriteLine($"Performance: {Performance}");
            Console.WriteLine($"Esforco: {Effort}");
            Console.WriteLine($"Frustracao: {Frustration}");
        }
    }
}