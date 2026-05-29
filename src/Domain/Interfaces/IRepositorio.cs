namespace AstroHabitsDesktop.Domain.Interfaces;

/// <summary>
/// Interface genérica para persistência de dados.
/// Garante desacoplamento entre domínio e infraestrutura.
/// </summary>
public interface IRepositorio<T> where T : class
{
    List<T> ObterTodos();
    T? ObterPorId(Guid id);
    void Adicionar(T entidade);
    void Atualizar(T entidade);
    void Remover(Guid id);
    void Salvar();
}
