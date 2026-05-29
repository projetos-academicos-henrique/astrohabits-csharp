namespace AstroHabitsDesktop.Partial;

/// <summary>
/// Parte de logs/histórico do Gerenciador de Missão.
/// Demonstra o uso de classes parciais separando responsabilidades.
/// </summary>
public partial class GerenciadorMissao
{
    private readonly List<string> _logs = new();

    public IReadOnlyList<string> Logs => _logs.AsReadOnly();

    /// <summary>
    /// Registra uma entrada de log com timestamp.
    /// </summary>
    public void RegistrarLog(string mensagem)
    {
        string entrada = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {mensagem}";
        _logs.Add(entrada);
    }

    /// <summary>
    /// Retorna os últimos N logs.
    /// </summary>
    public List<string> ObterLogsRecentes(int quantidade = 10)
    {
        return _logs.TakeLast(quantidade).Reverse().ToList();
    }

    /// <summary>
    /// Limpa todo o histórico de logs.
    /// </summary>
    public void LimparLogs()
    {
        _logs.Clear();
        RegistrarLog("Histórico de logs limpo.");
    }

    /// <summary>
    /// Exporta os logs como uma string formatada.
    /// </summary>
    public string ExportarLogs()
    {
        return string.Join(Environment.NewLine, _logs);
    }
}
