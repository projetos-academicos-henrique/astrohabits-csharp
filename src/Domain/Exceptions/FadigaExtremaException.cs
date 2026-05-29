namespace AstroHabitsDesktop.Domain.Exceptions;

/// <summary>
/// Exceção disparada quando o score Micro-TLX ultrapassa o limiar crítico (8.5),
/// indicando que o astronauta deve pausar antes de iniciar um novo ciclo.
/// </summary>
public class FadigaExtremaException : Exception
{
    public double ScoreAtual { get; }

    public FadigaExtremaException(double scoreAtual)
        : base($"⚠️ Fadiga Extrema detectada! Score TLX: {scoreAtual:F1}/10. " +
               "O astronauta deve realizar uma pausa obrigatória antes de continuar.")
    {
        ScoreAtual = scoreAtual;
    }

    public FadigaExtremaException(double scoreAtual, Exception innerException)
        : base($"⚠️ Fadiga Extrema detectada! Score TLX: {scoreAtual:F1}/10.", innerException)
    {
        ScoreAtual = scoreAtual;
    }
}
