using System.Collections.ObjectModel;
using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Domain.Enums;
using AstroHabitsDesktop.Domain.ModosFoco;
using AstroHabitsDesktop.Domain.Static;
using AstroHabitsDesktop.Infrastructure.Data;
using AstroHabitsDesktop.Partial;

namespace AstroHabitsDesktop.Presentation.ViewModels;

/// <summary>
/// ViewModel principal — gerencia navegação sidebar e estado global.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly GerenciadorMissao _gerenciador;
    private readonly NotificacaoManager _notificacaoManager;
    private readonly JsonRepositorio<Tarefa> _repoTarefas;
    private readonly JsonRepositorio<MicroTlxRegistro> _repoTlx;

    private ViewModelBase _currentView = null!;
    private string _selectedMenuItem = "Dashboard";
    private string _faseCircadianaTexto = "";
    private string _modoOrbitaTexto = "";

    // Child ViewModels
    public DashboardViewModel DashboardVM { get; }
    public TarefasViewModel TarefasVM { get; }
    public ModoFocoViewModel ModoFocoVM { get; }
    public MicroTlxViewModel MicroTlxVM { get; }
    public HistoricoViewModel HistoricoVM { get; }

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public string SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            if (SetProperty(ref _selectedMenuItem, value))
                NavigateTo(value);
        }
    }

    public string FaseCircadianaTexto
    {
        get => _faseCircadianaTexto;
        set => SetProperty(ref _faseCircadianaTexto, value);
    }

    public string ModoOrbitaTexto
    {
        get => _modoOrbitaTexto;
        set => SetProperty(ref _modoOrbitaTexto, value);
    }

    // Commands
    public RelayCommand NavDashboardCommand { get; }
    public RelayCommand NavTarefasCommand { get; }
    public RelayCommand NavModoFocoCommand { get; }
    public RelayCommand NavMicroTlxCommand { get; }
    public RelayCommand NavHistoricoCommand { get; }

    public MainViewModel()
    {
        _gerenciador = new GerenciadorMissao();
        _notificacaoManager = new NotificacaoManager();
        _repoTarefas = new JsonRepositorio<Tarefa>("tarefas");
        _repoTlx = new JsonRepositorio<MicroTlxRegistro>("microtlx");

        // Initialize child ViewModels
        DashboardVM = new DashboardViewModel(_gerenciador, _repoTarefas, _repoTlx);
        TarefasVM = new TarefasViewModel(_repoTarefas);
        ModoFocoVM = new ModoFocoViewModel(_gerenciador, _notificacaoManager);
        MicroTlxVM = new MicroTlxViewModel(_gerenciador, _repoTlx);
        HistoricoVM = new HistoricoViewModel(_repoTlx, _gerenciador);

        // Navigation commands
        NavDashboardCommand = new RelayCommand(() => SelectedMenuItem = "Dashboard");
        NavTarefasCommand = new RelayCommand(() => SelectedMenuItem = "Tarefas");
        NavModoFocoCommand = new RelayCommand(() => SelectedMenuItem = "Modo Foco");
        NavMicroTlxCommand = new RelayCommand(() => SelectedMenuItem = "Micro-TLX");
        NavHistoricoCommand = new RelayCommand(() => SelectedMenuItem = "Histórico");

        // Start on Dashboard
        CurrentView = DashboardVM;
        AtualizarEstadoGlobal();
    }

    private void NavigateTo(string view)
    {
        CurrentView = view switch
        {
            "Dashboard" => DashboardVM,
            "Tarefas" => TarefasVM,
            "Modo Foco" => ModoFocoVM,
            "Micro-TLX" => MicroTlxVM,
            "Histórico" => HistoricoVM,
            _ => DashboardVM
        };

        // Refresh data when navigating
        if (CurrentView == DashboardVM) DashboardVM.Refresh();
        if (CurrentView == HistoricoVM) HistoricoVM.Refresh();
        if (CurrentView == TarefasVM) TarefasVM.Refresh();

        AtualizarEstadoGlobal();
    }

    private void AtualizarEstadoGlobal()
    {
        var fase = GerenciadorMissao.ObterFaseCircadianaAtual();
        FaseCircadianaTexto = fase switch
        {
            FaseCircadiana.FocoIntenso => "🔥 Foco Intenso",
            FaseCircadiana.Transicao => "🔄 Transição",
            FaseCircadiana.FocoModerado => "⚡ Foco Moderado",
            FaseCircadiana.Descanso => "🌙 Descanso",
            _ => "Desconhecido"
        };
        ModoOrbitaTexto = _gerenciador.ModoAtual.Nome;
    }
}
