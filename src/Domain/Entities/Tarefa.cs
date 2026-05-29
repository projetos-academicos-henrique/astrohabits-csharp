namespace AstroHabitsDesktop.Domain.Entities;

/// <summary>
/// Representa uma tarefa/missão do astronauta.
/// Registra timestamps de início e conclusão para cálculo de TimeSpan.
/// </summary>
public class Tarefa
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public StatusTarefa Status { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataConclusao { get; private set; }
    public int Prioridade { get; private set; }

    public Tarefa(string titulo, string descricao, int prioridade = 1)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Status = StatusTarefa.Pendente;
        DataCriacao = DateTime.Now;
        Prioridade = Math.Clamp(prioridade, 1, 5);
    }

    public void Iniciar()
    {
        Status = StatusTarefa.EmAndamento;
        DataInicio = DateTime.Now;
    }

    public void Concluir()
    {
        Status = StatusTarefa.Concluida;
        DataConclusao = DateTime.Now;
    }

    public void Atualizar(string titulo, string descricao, int prioridade)
    {
        Titulo = titulo;
        Descricao = descricao;
        Prioridade = Math.Clamp(prioridade, 1, 5);
    }

    /// <summary>
    /// Calcula o tempo gasto na tarefa usando TimeSpan.
    /// </summary>
    public TimeSpan? CalcularTempoGasto()
    {
        if (DataInicio == null) return null;
        var fim = DataConclusao ?? DateTime.Now;
        return fim - DataInicio.Value;
    }
}

public enum StatusTarefa
{
    Pendente,
    EmAndamento,
    Concluida
}
