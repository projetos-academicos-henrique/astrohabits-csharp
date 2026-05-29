namespace AstroHabitsDesktop.Domain.Exceptions;

/// <summary>
/// Exceção disparada quando o simulador de delay de Marte falha,
/// representando uma falha na transmissão de comunicação de longa distância.
/// </summary>
public class FalhaTransmissaoMarteException : Exception
{
    public int DelayEsperado { get; }

    public FalhaTransmissaoMarteException(int delayEsperado)
        : base($"📡 Falha na transmissão para Marte! Delay esperado: {delayEsperado} min. " +
               "A comunicação não pôde ser estabelecida neste ciclo.")
    {
        DelayEsperado = delayEsperado;
    }

    public FalhaTransmissaoMarteException(int delayEsperado, Exception innerException)
        : base($"📡 Falha na transmissão para Marte! Delay esperado: {delayEsperado} min.", innerException)
    {
        DelayEsperado = delayEsperado;
    }
}
