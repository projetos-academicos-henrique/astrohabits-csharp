using AstroHabitsDesktop.Domain.Exceptions;

namespace AstroHabitsDesktop.Domain.Static;

/// <summary>
/// Classe estática utilitária com métodos matemáticos puros
/// para processar os scores do Micro-TLX.
/// </summary>
public static class CalculadoraCargaMental
{
    // Pesos para cada dimensão do Micro-TLX reduzido
    private const double PesoDemandaMental = 1.0;
    private const double PesoEsforco = 0.8;
    private const double PesoFrustracao = 0.6;
    private const double LimiarFadigaExtrema = 8.5;

    /// <summary>
    /// Calcula o score ponderado a partir das 3 dimensões do Micro-TLX.
    /// Fórmula: (mental * 1.0 + esforco * 0.8 + frustracao * 0.6) / (1.0 + 0.8 + 0.6)
    /// </summary>
    public static double CalcularScorePonderado(double demandaMental, double esforco, double frustracao)
    {
        double somaPesos = PesoDemandaMental + PesoEsforco + PesoFrustracao;
        double somaValores = (demandaMental * PesoDemandaMental)
                           + (esforco * PesoEsforco)
                           + (frustracao * PesoFrustracao);

        return Math.Round(somaValores / somaPesos, 2);
    }

    /// <summary>
    /// Classifica o nível de carga cognitiva com base no score ponderado.
    /// </summary>
    public static string ClassificarCarga(double score) => score switch
    {
        <= 2.5 => "Baixa",
        <= 5.0 => "Moderada",
        <= 7.5 => "Alta",
        _      => "Crítica"
    };

    /// <summary>
    /// Verifica se o score indica fadiga extrema e dispara a exceção customizada.
    /// </summary>
    public static bool VerificarFadigaExtrema(double score)
    {
        if (score > LimiarFadigaExtrema)
        {
            throw new FadigaExtremaException(score);
        }
        return false;
    }

    /// <summary>
    /// Retorna a cor associada ao nível de carga (para a interface).
    /// </summary>
    public static string ObterCorCarga(double score) => score switch
    {
        <= 2.5 => "#4CAF50", // Verde
        <= 5.0 => "#F5A623", // Laranja
        <= 7.5 => "#E8772E", // Laranja escuro
        _      => "#E53935"  // Vermelho
    };

    /// <summary>
    /// Calcula a média de um histórico de scores.
    /// </summary>
    public static double CalcularMediaHistorica(List<double> scores)
    {
        if (scores == null || scores.Count == 0) return 0;
        return Math.Round(scores.Average(), 2);
    }

    /// <summary>
    /// Determina a tendência (melhora/piora) comparando últimos N registros.
    /// </summary>
    public static string AnalisarTendencia(List<double> scores, int ultimosN = 3)
    {
        if (scores == null || scores.Count < 2) return "Dados insuficientes";

        var recentes = scores.TakeLast(Math.Min(ultimosN, scores.Count)).ToList();
        if (recentes.Count < 2) return "Dados insuficientes";

        double diff = recentes.Last() - recentes.First();
        return diff switch
        {
            < -1.0 => "📉 Melhorando",
            > 1.0  => "📈 Piorando",
            _      => "➡️ Estável"
        };
    }
}
