using AstroHabitsDesktop.Domain.Interfaces;

namespace AstroHabitsDesktop.Infrastructure.Data;

/// <summary>
/// Implementação de INotificacaoManager.
/// Gerencia a fila de notificações assíncronas durante modos de foco.
/// Usa foreach para varrer a fila conforme requisito.
/// </summary>
public class NotificacaoManager : INotificacaoManager
{
    private readonly Queue<string> _filaPendentes = new();

    /// <summary>
    /// Retém uma notificação na fila.
    /// </summary>
    public void ReterNotificacao(string mensagem)
    {
        if (!string.IsNullOrWhiteSpace(mensagem))
        {
            string notificacao = $"[{DateTime.Now:HH:mm:ss}] {mensagem}";
            _filaPendentes.Enqueue(notificacao);
        }
    }

    /// <summary>
    /// Despacha todas as notificações acumuladas.
    /// Usa foreach para varrer a fila (requisito do .rules).
    /// </summary>
    public List<string> DespacharFila()
    {
        var despachadas = new List<string>();

        // Foreach para varrer a fila de notificações acumuladas
        foreach (var notificacao in _filaPendentes)
        {
            despachadas.Add(notificacao);
        }

        _filaPendentes.Clear();
        return despachadas;
    }

    /// <summary>
    /// Retorna o número de notificações pendentes.
    /// </summary>
    public int ContarPendentes() => _filaPendentes.Count;

    /// <summary>
    /// Limpa toda a fila de notificações.
    /// </summary>
    public void LimparFila() => _filaPendentes.Clear();
}
