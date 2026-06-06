namespace astroHabitsCsharp.model
{
    
    
    
    public struct OrbitData
    {
        public string Name;            
        public double AltitudeKm;      
        public double VelocidadeKms;   
        public int DelaySeconds;       
        public int DelayRealMinutes;   

        
        public OrbitData(string name, double altitude, double velocidade, int delaySeconds, int delayRealMinutes)
        {
            Name = name;
            AltitudeKm = altitude;
            VelocidadeKms = velocidade;
            DelaySeconds = delaySeconds;
            DelayRealMinutes = delayRealMinutes;
        }

        public string GetInfo()
        {
            return $"{Name} -> Distancia: {AltitudeKm:N0}km | Velocidade: {VelocidadeKms}km/s | Delay: {GetDelayFormatted()}";
        }

        
        
        
        public string GetDelayFormatted()
        {
            if (DelayRealMinutes == 0)
                return "Instantaneo";
            return $"~{DelayRealMinutes} minutos";
        }

        
        
        

        
        public static readonly OrbitData Terra = new OrbitData(
            "Terra (ISS)", 400, 7.66,
            delaySeconds: 0,
            delayRealMinutes: 0
        );

        
        public static readonly OrbitData Lua = new OrbitData(
            "Lua", 384_400, 1.02,
            delaySeconds: 8,     
            delayRealMinutes: 8  
        );

        
        public static readonly OrbitData Marte = new OrbitData(
            "Marte", 225_000_000, 24.07,
            delaySeconds: 15,     
            delayRealMinutes: 20  
        );
    }
}
