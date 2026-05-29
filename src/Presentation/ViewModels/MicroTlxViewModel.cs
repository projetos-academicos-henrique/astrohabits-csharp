using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Domain.Static;
using AstroHabitsDesktop.Infrastructure.Data;
using AstroHabitsDesktop.Partial;

namespace AstroHabitsDesktop.Presentation.ViewModels;

public class MicroTlxViewModel : ViewModelBase
{
    private readonly GerenciadorMissao _gerenciador;
    private readonly JsonRepositorio<MicroTlxRegistro> _repo;
    private double _demandaMental = 5;
    private double _esforco = 5;
    private double _frustracao = 5;
    private string _scoreAtual = "—";
    private string _classificacao = "—";
    private string _corScore = "#4CAF50";
    private string _mensagem = "";
    private bool _fadigaDetectada;

    public double DemandaMental
    {
        get => _demandaMental;
        set { SetProperty(ref _demandaMental, value); AtualizarPreview(); }
    }
    public double Esforco
    {
        get => _esforco;
        set { SetProperty(ref _esforco, value); AtualizarPreview(); }
    }
    public double Frustracao
    {
        get => _frustracao;
        set { SetProperty(ref _frustracao, value); AtualizarPreview(); }
    }
    public string ScoreAtual { get => _scoreAtual; set => SetProperty(ref _scoreAtual, value); }
    public string Classificacao { get => _classificacao; set => SetProperty(ref _classificacao, value); }
    public string CorScore { get => _corScore; set => SetProperty(ref _corScore, value); }
    public string Mensagem { get => _mensagem; set => SetProperty(ref _mensagem, value); }
    public bool FadigaDetectada { get => _fadigaDetectada; set => SetProperty(ref _fadigaDetectada, value); }

    public RelayCommand RegistrarCommand { get; }
    public RelayCommand ResetarCommand { get; }

    public MicroTlxViewModel(GerenciadorMissao gerenciador, JsonRepositorio<MicroTlxRegistro> repo)
    {
        _gerenciador = gerenciador;
        _repo = repo;
        RegistrarCommand = new RelayCommand(Registrar);
        ResetarCommand = new RelayCommand(Resetar);
        AtualizarPreview();
    }

    private void AtualizarPreview()
    {
        double score = CalculadoraCargaMental.CalcularScorePonderado(DemandaMental, Esforco, Frustracao);
        ScoreAtual = $"{score:F1}";
        Classificacao = CalculadoraCargaMental.ClassificarCarga(score);
        CorScore = CalculadoraCargaMental.ObterCorCarga(score);
        FadigaDetectada = score > 8.5;
    }

    private void Registrar()
    {
        var (registro, erro) = _gerenciador.RegistrarMicroTlx(DemandaMental, Esforco, Frustracao);

        if (registro != null)
        {
            _repo.Adicionar(registro);
        }

        if (erro != null)
        {
            Mensagem = $"⚠️ {erro}";
            FadigaDetectada = true;
        }
        else
        {
            Mensagem = $"✅ Registro salvo! Score: {registro!.ScorePonderado:F1} ({registro.Classificacao})";
            FadigaDetectada = false;
        }
    }

    private void Resetar()
    {
        DemandaMental = 5;
        Esforco = 5;
        Frustracao = 5;
        Mensagem = "";
        FadigaDetectada = false;
    }
}
