using System.Collections.ObjectModel;
using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Domain.Static;
using AstroHabitsDesktop.Infrastructure.Data;
using AstroHabitsDesktop.Partial;

namespace AstroHabitsDesktop.Presentation.ViewModels;

public class HistoricoViewModel : ViewModelBase
{
    private readonly JsonRepositorio<MicroTlxRegistro> _repo;
    private readonly GerenciadorMissao _gerenciador;
    private string _mediaGeral = "—";
    private string _tendencia = "—";
    private int _totalRegistros;

    public ObservableCollection<MicroTlxRegistro> Registros { get; } = new();
    public ObservableCollection<string> Logs { get; } = new();

    public string MediaGeral { get => _mediaGeral; set => SetProperty(ref _mediaGeral, value); }
    public string Tendencia { get => _tendencia; set => SetProperty(ref _tendencia, value); }
    public int TotalRegistros { get => _totalRegistros; set => SetProperty(ref _totalRegistros, value); }

    public RelayCommand LimparHistoricoCommand { get; }

    public HistoricoViewModel(JsonRepositorio<MicroTlxRegistro> repo, GerenciadorMissao gerenciador)
    {
        _repo = repo;
        _gerenciador = gerenciador;
        LimparHistoricoCommand = new RelayCommand(LimparHistorico);
        Refresh();
    }

    public void Refresh()
    {
        Registros.Clear();
        var todos = _repo.ObterTodos();
        foreach (var r in todos.OrderByDescending(r => r.Timestamp))
            Registros.Add(r);

        TotalRegistros = todos.Count;

        if (todos.Count > 0)
        {
            var scores = todos.Select(r => r.ScorePonderado).ToList();
            MediaGeral = $"{CalculadoraCargaMental.CalcularMediaHistorica(scores):F1}";
            Tendencia = CalculadoraCargaMental.AnalisarTendencia(scores);
        }
        else
        {
            MediaGeral = "—";
            Tendencia = "Sem dados";
        }

        Logs.Clear();
        foreach (var log in _gerenciador.ObterLogsRecentes(20))
            Logs.Add(log);
    }

    private void LimparHistorico()
    {
        // Remove all records from repo
        foreach (var r in _repo.ObterTodos())
            _repo.Remover(r.Id);
        Refresh();
    }
}
