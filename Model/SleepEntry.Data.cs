using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace astroHabitsCsharp.model
{
    
    
    public partial class SleepEntry
    {
        
        private const string sleepFilePath = "sono.json";

        
        public static void SalvarDadosSono()
        {
            try
            {
                
                string jsonString = JsonSerializer.Serialize(listOfSleep, new JsonSerializerOptions { WriteIndented = true });
                
                File.WriteAllText(sleepFilePath, jsonString);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao salvar os dados de sono: " + ex.Message);
            }
        }

        
        public static void CarregarDadosSono()
        {
            try
            {
                if (File.Exists(sleepFilePath))
                {
                    
                    string jsonString = File.ReadAllText(sleepFilePath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        
                        var dadosCarregados = JsonSerializer.Deserialize<List<SleepEntry>>(jsonString);
                        if (dadosCarregados != null)
                        {
                            listOfSleep = dadosCarregados;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao carregar os dados de sono: " + ex.Message);
            }
        }
    }
}
