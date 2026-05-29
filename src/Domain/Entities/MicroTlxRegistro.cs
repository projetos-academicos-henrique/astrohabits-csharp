namespace AstroHabitsDesktop.Domain.Entities;

/// <summary>
/// Registro de uma resposta do questionário Micro-TLX (versão reduzida).
/// Armazena as 3 dimensões e o score calculado com timestamp.
/// </summary>
public class MicroTlxRegistro
{
    public Guid Id { get; private set; }
    public DateTime Timestamp { get; private set; }

    /// <summary>Demanda Mental (0–10)</summary>
    public double DemandaMental { get; private set; }

    /// <summary>Esforço (0–10)</summary>
    public double Esforco { get; private set; }

    /// <summary>Frustração (0–10)</summary>
    public double Frustracao { get; private set; }

    /// <summary>Score ponderado calculado</summary>
    public double ScorePonderado { get; private set; }

    /// <summary>Classificação textual da carga</summary>
    public string Classificacao { get; private set; }

    /// <summary>Modo de órbita ativo no momento do registro</summary>
    public string ModoOrbita { get; private set; }

    public MicroTlxRegistro(
        double demandaMental,
        double esforco,
        double frustracao,
        double scorePonderado,
        string classificacao,
        string modoOrbita)
    {
        Id = Guid.NewGuid();
        Timestamp = DateTime.Now;
        DemandaMental = Math.Clamp(demandaMental, 0, 10);
        Esforco = Math.Clamp(esforco, 0, 10);
        Frustracao = Math.Clamp(frustracao, 0, 10);
        ScorePonderado = scorePonderado;
        Classificacao = classificacao;
        ModoOrbita = modoOrbita;
    }
}
