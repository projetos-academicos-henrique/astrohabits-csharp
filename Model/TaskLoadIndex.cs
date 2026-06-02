namespace astroHabitsCsharp.model
{
    public class TaskLoadIndex : Entry
    {
        private String name;
        private int mentalDemand;
        private int physicalDemand;
        private int temporalDemand;
        private int performance;
        private int effort;
        private int frustration;

        private static TaskLoadIndex[] listOfTLX; 

        public TaskLoadIndex(string name, int mentalDemand, int physicalDemand, int temporalDemand, int performance, int effort, int frustration)
        {
            this.name = name;
            this.mentalDemand = mentalDemand;
            this.physicalDemand = physicalDemand;
            this.temporalDemand = temporalDemand;
            this.performance = performance;
            this.effort = effort;
            this.frustration = frustration;

            listOfTLX.Append(this);
        }

        public static void exibirTodosTLX()
        {
            foreach (var item in listOfTLX)
            {
                item.print();
            }
        }

        public void print()
        {
            Console.WriteLine($"Nome: {name}");
            Console.WriteLine($"Demanda Mental: {mentalDemand}");
            Console.WriteLine($"Demanda Fisica: {physicalDemand}");
            Console.WriteLine($"Demanda de tempo: {temporalDemand}");
            Console.WriteLine($"Perfomace: {performance}");
            Console.WriteLine($"Esforco: {effort}");
            Console.WriteLine($"Frustracao: {frustration}");
            
        }
    }
}