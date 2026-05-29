namespace AstroHabitsDesktop.Domain.ModosFoco;

/// <summary>
/// Classe abstrata que define a regra base para qualquer modo de foco/órbita.
/// Demonstra herança e polimorfismo via método abstrato.
/// </summary>
public abstract class ModoFocoBase
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Icone { get; private set; }

    protected ModoFocoBase(string nome, string descricao, string icone)
    {
        Nome = nome;
        Descricao = descricao;
        Icone = icone;
    }

    /// <summary>
    /// Método abstrato que retorna o delay em minutos para notificações.
    /// Cada modo de órbita implementa seu próprio delay (polimorfismo).
    /// </summary>
    public abstract int CalcularDelayNotificacao();

    /// <summary>
    /// Método virtual que pode ser sobrescrito para customizar a descrição.
    /// </summary>
    public virtual string ObterDescricaoCompleta()
        => $"[{Nome}] {Descricao} — Delay: {CalcularDelayNotificacao()} min";
}
