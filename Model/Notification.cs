using System;

namespace astroHabitsCsharp.model
{
    
    
    
    public class Notification
    {
        
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public bool IsDelivered { get; set; }

        
        public Notification(string message)
        {
            this.Message = message;
            this.Timestamp = DateTime.Now.ToString("HH:mm:ss");
            this.IsDelivered = false;
        }

        
        
        private static readonly string[] mensagensSimuladas = new string[]
        {
            "Novo e-mail: 'Reuniao remarcada para amanha'",
            "WhatsApp: 'E ai, vamos almocar hoje?'",
            "Instagram: Fulano curtiu sua foto",
            "Slack: Nova mensagem no canal #geral",
            "News: 'SpaceX anuncia nova missao para Marte'",
            "Telegram: 'Oi! Tudo bem?'",
            "Novo e-mail: 'Promocao imperdivel!'",
            "Teams: 'Daily em 15 minutos'",
            "Twitter: Alguem mencionou voce",
            "WhatsApp: 'Viu o jogo ontem?'",
            "Novo e-mail: 'Feedback do projeto'",
            "Discord: Nova mensagem no servidor",
            "WhatsApp: 'Preciso de ajuda com uma coisa'",
            "Novo e-mail: 'Relatorio semanal disponivel'",
            "Jira: Tarefa atualizada",
            "SMS: 'Sua encomenda foi entregue'",
            "YouTube: Novo video do canal que voce segue"
        };

        
        private static readonly Random random = new Random();

        
        
        
        
        public static Notification GerarAleatoria()
        {
            int index = random.Next(mensagensSimuladas.Length);
            return new Notification(mensagensSimuladas[index]);
        }

        
        
        
        public void Entregar()
        {
            this.IsDelivered = true;
        }

        
        
        
        public override string ToString()
        {
            return $"  [{Timestamp}] {Message}";
        }
    }
}
