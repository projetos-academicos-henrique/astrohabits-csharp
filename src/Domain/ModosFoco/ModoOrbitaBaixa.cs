namespace AstroHabitsDesktop.Domain.ModosFoco;

/// <summary>
/// Modo Órbita Baixa: comunicação instantânea, sem delay.
/// Ideal para tarefas colaborativas em tempo real.
/// </summary>
public class ModoOrbitaBaixa : ModoFocoBase
{
    public ModoOrbitaBaixa()
        : base(
            "Órbita Baixa",
            "Comunicação em tempo real. Notificações instantâneas.",
            "🛰️")
    { }

    public override int CalcularDelayNotificacao() => 0;

    public override string ObterDescricaoCompleta()
        => $"🛰️ {Nome}: Sem delay de comunicação. Todas as notificações chegam imediatamente.";
}
