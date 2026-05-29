namespace AstroHabitsDesktop.Domain.Entities;

/// <summary>
/// Struct leve e imutável para armazenar dados de tempo de tela
/// e nível de luz azul recomendado para aquele momento.
/// </summary>
public readonly struct CoordenadasTempo
{
    public TimeSpan TempoTela { get; }
    public double NivelLuzAzul { get; }
    public DateTime Timestamp { get; }

    public CoordenadasTempo(TimeSpan tempoTela, double nivelLuzAzul, DateTime timestamp)
    {
        TempoTela = tempoTela;
        NivelLuzAzul = Math.Clamp(nivelLuzAzul, 0.0, 1.0);
        Timestamp = timestamp;
    }

    public override string ToString()
        => $"[{Timestamp:HH:mm}] Tela: {TempoTela:hh\\:mm}, Luz Azul: {NivelLuzAzul:P0}";
}
