namespace AstroHabitsDesktop.Domain.Interfaces;

/// <summary>
/// Interface para gerenciamento de notificações assíncronas.
/// Permite reter notificações durante modos de foco e despachá-las depois.
/// </summary>
public interface INotificacaoManager
{
    /// <summary>
    /// Retém uma notificação na fila enquanto o modo de foco está ativo.
    /// </summary>
    void ReterNotificacao(string mensagem);

    /// <summary>
    /// Despacha todas as notificações acumuladas na fila.
    /// </summary>
    List<string> DespacharFila();

    /// <summary>
    /// Retorna o número de notificações pendentes na fila.
    /// </summary>
    int ContarPendentes();

    /// <summary>
    /// Limpa toda a fila de notificações.
    /// </summary>
    void LimparFila();
}
