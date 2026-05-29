namespace AstroHabitsDesktop.Domain.ModosFoco;

/// <summary>
/// Modo Lua: delay moderado de 5 minutos.
/// Ideal para tarefas que exigem foco intermediário.
/// </summary>
public class ModoLua : ModoFocoBase
{
    public ModoLua()
        : base(
            "Lua",
            "Delay moderado. Notificações acumuladas a cada 5 minutos.",
            "🌙")
    { }

    public override int CalcularDelayNotificacao() => 5;

    public override string ObterDescricaoCompleta()
        => $"🌙 {Nome}: Delay de {CalcularDelayNotificacao()} min. Notificações são acumuladas e entregues em lotes.";
}
