namespace AstroHabitsDesktop.Domain.Entities;

/// <summary>
/// Representa o astronauta/usuário do sistema.
/// Encapsulamento rigoroso com propriedades get; private set.
/// </summary>
public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Codinome { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public int TotalCiclosConcluidos { get; private set; }
    public double UltimoScoreTlx { get; private set; }

    public Usuario(string nome, string codinome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Codinome = codinome;
        DataCadastro = DateTime.Now;
        TotalCiclosConcluidos = 0;
        UltimoScoreTlx = 0;
    }

    public void RegistrarCiclo(double scoreTlx)
    {
        TotalCiclosConcluidos++;
        UltimoScoreTlx = scoreTlx;
    }

    public void AtualizarCodinome(string novoCodinome)
    {
        Codinome = novoCodinome;
    }
}
