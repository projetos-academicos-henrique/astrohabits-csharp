using System;
using astroHabitsCsharp.Exceptions;

namespace astroHabitsCsharp.util
{
    
    
    static public class SleepCalculator
    {
        
        private const int SCREEN_CUTOFF_HOURS = 2;

        

        
        
        
        
        static public TimeSpan CalculateScreenCutoff(TimeSpan desiredSleepTime)
        {
            return desiredSleepTime.Subtract(TimeSpan.FromHours(SCREEN_CUTOFF_HOURS));
        }

        
        
        
        
        static public TimeSpan TimeUntilCutoff(TimeSpan desiredSleepTime)
        {
            TimeSpan cutoff = CalculateScreenCutoff(desiredSleepTime);
            TimeSpan now = DateTime.Now.TimeOfDay;

            TimeSpan diff = cutoff - now;
            if (diff.TotalMinutes < 0)
            {
                
                return TimeSpan.Zero;
            }
            return diff;
        }

        

        
        
        
        
        
        
        static public string GetCurrentPhase()
        {
            int hour = DateTime.Now.Hour;

            if (hour >= 6 && hour < 18)
                return "Dia";
            else if (hour >= 18 && hour < 21)
                return "Entardecer";
            else
                return "Noite";
        }

        
        
        
        static public string[] GetTips(string phase)
        {
            switch (phase)
            {
                case "Dia":
                    return new string[]
                    {
                        "Exposicao a luz natural e bem-vinda. Aproveite para trabalhar perto de janelas.",
                        "Produtividade esta no pico - foque nas tarefas mais exigentes agora.",
                        "Mantenha-se hidratado(a)."
                    };
                case "Entardecer":
                    return new string[]
                    {
                        "Comece a reduzir o brilho das telas gradualmente.",
                        "Ative o modo noturno / filtro de luz azul nos seus dispositivos.",
                        "Considere trocar telas por atividades analogicas (leitura, conversa).",
                        "Evite cafeina a partir de agora."
                    };
                case "Noite":
                    return new string[]
                    {
                        "Hora de desligar! Seu corpo precisa preparar a melatonina.",
                        "A luz azul das telas suprime a producao de melatonina em ate 50%.",
                        "Se precisa usar o celular, use brilho minimo + filtro de luz azul.",
                        "Tente uma rotina pre-sono: 10 min de respiracao ou alongamento."
                    };
                default:
                    return new string[] { "Mantenha habitos saudaveis!" };
            }
        }

        

        
        
        
        
        static public TimeSpan ParseTime(string input)
        {
            
            if (TimeSpan.TryParseExact(input, "h\\:mm", null, out TimeSpan result))
            {
                return result;
            }

            
            if (input.Length == 4 && int.TryParse(input, out int numeric))
            {
                int hours = numeric / 100;
                int minutes = numeric % 100;

                if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    return new TimeSpan(hours, minutes, 0);
                }
            }

            
            throw new InvalidTimeException($"Horario invalido: '{input}'. Use o formato HH:mm (ex: 23:00 ou 2300).");
        }

        

        
        
        
        static public string GetSleepQualityLabel(int quality)
        {
            if (quality <= 3)
                return "Ruim";
            else if (quality <= 5)
                return "Regular";
            else if (quality <= 7)
                return "Boa";
            else if (quality <= 9)
                return "Otima";
            else
                return "Excelente";
        }
    }
}
