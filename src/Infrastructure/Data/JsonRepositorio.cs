using System.Text.Json;
using AstroHabitsDesktop.Domain.Interfaces;

namespace AstroHabitsDesktop.Infrastructure.Data;

/// <summary>
/// Implementação de IRepositorio usando arquivos JSON locais.
/// Cada tipo T tem seu próprio arquivo de persistência.
/// </summary>
public class JsonRepositorio<T> : IRepositorio<T> where T : class
{
    private readonly string _caminhoArquivo;
    private List<T> _dados;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonRepositorio(string nomeArquivo)
    {
        string pastaApp = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AstroHabits", "data");

        Directory.CreateDirectory(pastaApp);
        _caminhoArquivo = Path.Combine(pastaApp, $"{nomeArquivo}.json");

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        _dados = CarregarDados();
    }

    public List<T> ObterTodos() => new(_dados);

    public T? ObterPorId(Guid id)
    {
        var propId = typeof(T).GetProperty("Id");
        if (propId == null) return null;

        return _dados.FirstOrDefault(item =>
        {
            var valor = propId.GetValue(item);
            return valor is Guid guid && guid == id;
        });
    }

    public void Adicionar(T entidade)
    {
        _dados.Add(entidade);
        Salvar();
    }

    public void Atualizar(T entidade)
    {
        var propId = typeof(T).GetProperty("Id");
        if (propId == null) return;

        var id = propId.GetValue(entidade);
        var index = _dados.FindIndex(item =>
        {
            var valor = propId.GetValue(item);
            return valor is Guid guid && id is Guid targetId && guid == targetId;
        });

        if (index >= 0)
        {
            _dados[index] = entidade;
            Salvar();
        }
    }

    public void Remover(Guid id)
    {
        var propIdInfo = typeof(T).GetProperty("Id");
        if (propIdInfo == null) return;

        _dados.RemoveAll(item =>
        {
            var valor = propIdInfo.GetValue(item);
            return valor is Guid guid && guid == id;
        });
        Salvar();
    }

    public void Salvar()
    {
        try
        {
            string json = JsonSerializer.Serialize(_dados, _jsonOptions);
            File.WriteAllText(_caminhoArquivo, json);
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine($"Erro ao salvar dados: {ex.Message}");
        }
    }

    private List<T> CarregarDados()
    {
        try
        {
            if (!File.Exists(_caminhoArquivo))
                return new List<T>();

            string json = File.ReadAllText(_caminhoArquivo);
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao carregar dados: {ex.Message}");
            return new List<T>();
        }
    }
}
