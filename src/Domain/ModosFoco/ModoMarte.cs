namespace AstroHabitsDesktop.Domain.ModosFoco;

/// <summary>
/// Modo Marte: delay máximo de 20 minutos.
/// Ideal para foco profundo sem interrupções.
/// </summary>
public class ModoMarte : ModoFocoBase
{
    public ModoMarte()
        : base(
            "Marte",
            "Delay máximo. Notificações retidas por 20 minutos para foco profundo.",
            "🔴")
    { }

    public override int CalcularDelayNotificacao() => 20;

    public override string ObterDescricaoCompleta()
        => $"🔴 {Nome}: Delay de {CalcularDelayNotificacao()} min. Modo de foco profundo — comunicação altamente assíncrona.";
}
