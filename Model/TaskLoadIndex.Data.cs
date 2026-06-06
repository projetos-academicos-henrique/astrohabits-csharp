using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace astroHabitsCsharp.model
{
    
    public partial class TaskLoadIndex
    {
        
        private const string filePath = "dados.json";

        
        public static void SalvarDados()
        {
            try
            {
                
                string jsonString = JsonSerializer.Serialize(listOfTLX, new JsonSerializerOptions { WriteIndented = true });
                
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao salvar os dados: " + ex.Message);
            }
        }

        
        public static void CarregarDados()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    
                    string jsonString = File.ReadAllText(filePath);
                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        
                        var dadosCarregados = JsonSerializer.Deserialize<List<TaskLoadIndex>>(jsonString);
                        if (dadosCarregados != null)
                        {
                            listOfTLX = dadosCarregados;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao carregar os dados: " + ex.Message);
            }
        }
    }
}
