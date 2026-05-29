using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Domain.Enums;
using AstroHabitsDesktop.Domain.Static;
using AstroHabitsDesktop.Infrastructure.Data;
using AstroHabitsDesktop.Partial;

namespace AstroHabitsDesktop.Presentation.ViewModels;

/// <summary>
/// ViewModel do Dashboard — resumo do estado atual da missão.
/// </summary>
public class DashboardViewModel : ViewModelBase
{
    private readonly GerenciadorMissao _gerenciador;
    private readonly JsonRepositorio<Tarefa> _repoTarefas;
    private readonly JsonRepositorio<MicroTlxRegistro> _repoTlx;

    private string _faseCircadiana = "";
    private string _faseDescricao = "";
    private string _modoOrbita = "";
    private string _modoOrbitaDetalhe = "";
    private int _delayAtual;
    private string _ultimoScoreTlx = "—";
    private string _classificacaoCarga = "—";
    private string _tendencia = "—";
    private int _totalTarefas;
    private int _tarefasPendentes;
    private int _tarefasConcluidas;
    private string _horaAtual = "";
    private string _corCarga = "#4CAF50";

    public string FaseCircadiana { get => _faseCircadiana; set => SetProperty(ref _faseCircadiana, value); }
    public string FaseDescricao { get => _faseDescricao; set => SetProperty(ref _faseDescricao, value); }
    public string ModoOrbita { get => _modoOrbita; set => SetProperty(ref _modoOrbita, value); }
    public string ModoOrbitaDetalhe { get => _modoOrbitaDetalhe; set => SetProperty(ref _modoOrbitaDetalhe, value); }
    public int DelayAtual { get => _delayAtual; set => SetProperty(ref _delayAtual, value); }
    public string UltimoScoreTlx { get => _ultimoScoreTlx; set => SetProperty(ref _ultimoScoreTlx, value); }
    public string ClassificacaoCarga { get => _classificacaoCarga; set => SetProperty(ref _classificacaoCarga, value); }
    public string Tendencia { get => _tendencia; set => SetProperty(ref _tendencia, value); }
    public int TotalTarefas { get => _totalTarefas; set => SetProperty(ref _totalTarefas, value); }
    public int TarefasPendentes { get => _tarefasPendentes; set => SetProperty(ref _tarefasPendentes, value); }
    public int TarefasConcluidas { get => _tarefasConcluidas; set => SetProperty(ref _tarefasConcluidas, value); }
    public string HoraAtual { get => _horaAtual; set => SetProperty(ref _horaAtual, value); }
    public string CorCarga { get => _corCarga; set => SetProperty(ref _corCarga, value); }

    public DashboardViewModel(
        GerenciadorMissao gerenciador,
        JsonRepositorio<Tarefa> repoTarefas,
        JsonRepositorio<MicroTlxRegistro> repoTlx)
    {
        _gerenciador = gerenciador;
        _repoTarefas = repoTarefas;
        _repoTlx = repoTlx;
        Refresh();
    }

    public void Refresh()
    {
        // Fase circadiana
        var fase = GerenciadorMissao.ObterFaseCircadianaAtual();
        FaseCircadiana = fase switch
        {
            Domain.Enums.FaseCircadiana.FocoIntenso => "🔥 Foco Intenso",
            Domain.Enums.FaseCircadiana.Transicao => "🔄 Transição",
            Domain.Enums.FaseCircadiana.FocoModerado => "⚡ Foco Moderado",
            Domain.Enums.FaseCircadiana.Descanso => "🌙 Descanso",
            _ => "—"
        };
        FaseDescricao = fase switch
        {
            Domain.Enums.FaseCircadiana.FocoIntenso => "Período de máxima performance cognitiva (08:00–12:00)",
            Domain.Enums.FaseCircadiana.Transicao => "Transição pós-almoço — reduzir carga (12:00–14:00)",
            Domain.Enums.FaseCircadiana.FocoModerado => "Performance moderada — tarefas regulares (14:00–18:00)",
            Domain.Enums.FaseCircadiana.Descanso => "Período de recuperação — evitar carga intensa (18:00–08:00)",
            _ => ""
        };

        // Modo de órbita
        ModoOrbita = _gerenciador.ModoAtual.Nome;
        ModoOrbitaDetalhe = _gerenciador.ModoAtual.ObterDescricaoCompleta();
        DelayAtual = _gerenciador.ModoAtual.CalcularDelayNotificacao();

        // Micro-TLX
        var registros = _repoTlx.ObterTodos();
        if (registros.Count > 0)
        {
            var ultimo = registros.Last();
            UltimoScoreTlx = $"{ultimo.ScorePonderado:F1}";
            ClassificacaoCarga = ultimo.Classificacao;
            CorCarga = CalculadoraCargaMental.ObterCorCarga(ultimo.ScorePonderado);
            Tendencia = CalculadoraCargaMental.AnalisarTendencia(
                registros.Select(r => r.ScorePonderado).ToList());
        }
        else
        {
            UltimoScoreTlx = "—";
            ClassificacaoCarga = "Sem dados";
            Tendencia = "Sem dados";
            CorCarga = "#4CAF50";
        }

        // Tarefas
        var tarefas = _repoTarefas.ObterTodos();
        TotalTarefas = tarefas.Count;
        TarefasPendentes = tarefas.Count(t => t.Status == StatusTarefa.Pendente);
        TarefasConcluidas = tarefas.Count(t => t.Status == StatusTarefa.Concluida);

        // Hora
        HoraAtual = DateTime.Now.ToString("HH:mm");
    }
}
