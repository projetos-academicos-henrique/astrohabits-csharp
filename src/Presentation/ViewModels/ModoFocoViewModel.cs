using System.Collections.ObjectModel;
using AstroHabitsDesktop.Domain.ModosFoco;
using AstroHabitsDesktop.Domain.Exceptions;
using AstroHabitsDesktop.Infrastructure.Data;
using AstroHabitsDesktop.Partial;

namespace AstroHabitsDesktop.Presentation.ViewModels;

public class ModoFocoViewModel : ViewModelBase
{
    private readonly GerenciadorMissao _gerenciador;
    private readonly NotificacaoManager _notificacaoManager;
    private string _modoAtualNome = "";
    private string _modoAtualDescricao = "";
    private int _delayAtual;
    private int _notificacoesPendentes;
    private string _mensagem = "";
    private string _novaNotificacao = "";

    public ObservableCollection<string> NotificacoesDespachadas { get; } = new();

    public string ModoAtualNome { get => _modoAtualNome; set => SetProperty(ref _modoAtualNome, value); }
    public string ModoAtualDescricao { get => _modoAtualDescricao; set => SetProperty(ref _modoAtualDescricao, value); }
    public int DelayAtual { get => _delayAtual; set => SetProperty(ref _delayAtual, value); }
    public int NotificacoesPendentes { get => _notificacoesPendentes; set => SetProperty(ref _notificacoesPendentes, value); }
    public string Mensagem { get => _mensagem; set => SetProperty(ref _mensagem, value); }
    public string NovaNotificacao { get => _novaNotificacao; set => SetProperty(ref _novaNotificacao, value); }

    public RelayCommand SelecionarOrbitaBaixaCommand { get; }
    public RelayCommand SelecionarLuaCommand { get; }
    public RelayCommand SelecionarMarteCommand { get; }
    public RelayCommand AdicionarNotificacaoCommand { get; }
    public RelayCommand DespacharFilaCommand { get; }
    public RelayCommand LimparFilaCommand { get; }

    public ModoFocoViewModel(GerenciadorMissao gerenciador, NotificacaoManager notificacaoManager)
    {
        _gerenciador = gerenciador;
        _notificacaoManager = notificacaoManager;

        SelecionarOrbitaBaixaCommand = new RelayCommand(() => AlterarModo(new ModoOrbitaBaixa()));
        SelecionarLuaCommand = new RelayCommand(() => AlterarModo(new ModoLua()));
        SelecionarMarteCommand = new RelayCommand(() => AlterarModo(new ModoMarte()));
        AdicionarNotificacaoCommand = new RelayCommand(AdicionarNotificacao);
        DespacharFilaCommand = new RelayCommand(DespacharFila);
        LimparFilaCommand = new RelayCommand(LimparFila);

        AtualizarEstado();
    }

    private void AlterarModo(ModoFocoBase modo)
    {
        try
        {
            _gerenciador.AlterarModo(modo);
            AtualizarEstado();
            Mensagem = $"✅ Modo alterado para {modo.Nome}";
        }
        catch (FalhaTransmissaoMarteException ex)
        {
            Mensagem = $"❌ {ex.Message}";
        }
        catch (Exception ex)
        {
            Mensagem = $"❌ Erro: {ex.Message}";
        }
    }

    private void AdicionarNotificacao()
    {
        if (string.IsNullOrWhiteSpace(NovaNotificacao)) { Mensagem = "⚠️ Digite uma notificação."; return; }
        _notificacaoManager.ReterNotificacao(NovaNotificacao.Trim());
        NovaNotificacao = "";
        NotificacoesPendentes = _notificacaoManager.ContarPendentes();
        Mensagem = $"📩 Notificação retida. Delay: {DelayAtual} min.";
    }

    private void DespacharFila()
    {
        var despachadas = _notificacaoManager.DespacharFila();
        NotificacoesDespachadas.Clear();
        foreach (var n in despachadas) NotificacoesDespachadas.Add(n);
        NotificacoesPendentes = 0;
        Mensagem = $"📬 {despachadas.Count} notificações despachadas!";
    }

    private void LimparFila()
    {
        _notificacaoManager.LimparFila();
        NotificacoesPendentes = 0;
        NotificacoesDespachadas.Clear();
        Mensagem = "🗑️ Fila limpa.";
    }

    private void AtualizarEstado()
    {
        ModoAtualNome = _gerenciador.ModoAtual.Nome;
        ModoAtualDescricao = _gerenciador.ModoAtual.ObterDescricaoCompleta();
        DelayAtual = _gerenciador.ModoAtual.CalcularDelayNotificacao();
        NotificacoesPendentes = _notificacaoManager.ContarPendentes();
    }
}
