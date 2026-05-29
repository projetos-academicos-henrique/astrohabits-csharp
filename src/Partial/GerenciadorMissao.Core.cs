using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Domain.Enums;
using AstroHabitsDesktop.Domain.Exceptions;
using AstroHabitsDesktop.Domain.ModosFoco;
using AstroHabitsDesktop.Domain.Static;

namespace AstroHabitsDesktop.Partial;

/// <summary>
/// Parte principal do Gerenciador de Missão — lógica de negócio central.
/// Demonstra o uso de classes parciais (partial class).
/// </summary>
public partial class GerenciadorMissao
{
    private ModoFocoBase _modoAtual;
    private readonly List<Tarefa> _tarefasAtivas;
    private readonly List<MicroTlxRegistro> _historicoTlx;

    public ModoFocoBase ModoAtual => _modoAtual;
    public IReadOnlyList<Tarefa> TarefasAtivas => _tarefasAtivas.AsReadOnly();
    public IReadOnlyList<MicroTlxRegistro> HistoricoTlx => _historicoTlx.AsReadOnly();

    public GerenciadorMissao()
    {
        _modoAtual = new ModoOrbitaBaixa();
        _tarefasAtivas = new List<Tarefa>();
        _historicoTlx = new List<MicroTlxRegistro>();
    }

    /// <summary>
    /// Altera o modo de órbita ativo (polimorfismo em ação).
    /// </summary>
    public void AlterarModo(ModoFocoBase novoModo)
    {
        _modoAtual = novoModo ?? throw new ArgumentNullException(nameof(novoModo));
        RegistrarLog($"Modo alterado para: {_modoAtual.ObterDescricaoCompleta()}");
    }

    /// <summary>
    /// Registra um novo score Micro-TLX, verificando fadiga extrema.
    /// Usa try-catch para capturar a exceção customizada.
    /// </summary>
    public (MicroTlxRegistro? registro, string? erro) RegistrarMicroTlx(
        double demandaMental, double esforco, double frustracao)
    {
        try
        {
            double score = CalculadoraCargaMental.CalcularScorePonderado(
                demandaMental, esforco, frustracao);

            string classificacao = CalculadoraCargaMental.ClassificarCarga(score);

            // Verifica se há fadiga extrema — pode disparar FadigaExtremaException
            CalculadoraCargaMental.VerificarFadigaExtrema(score);

            var registro = new MicroTlxRegistro(
                demandaMental, esforco, frustracao,
                score, classificacao, _modoAtual.Nome);

            _historicoTlx.Add(registro);
            RegistrarLog($"Micro-TLX registrado: Score {score:F1} ({classificacao})");

            return (registro, null);
        }
        catch (FadigaExtremaException ex)
        {
            RegistrarLog($"ALERTA: {ex.Message}");

            // Mesmo em fadiga extrema, registra o score para histórico
            double score = CalculadoraCargaMental.CalcularScorePonderado(
                demandaMental, esforco, frustracao);
            var registro = new MicroTlxRegistro(
                demandaMental, esforco, frustracao,
                score, "Crítica", _modoAtual.Nome);
            _historicoTlx.Add(registro);

            return (registro, ex.Message);
        }
        catch (Exception ex)
        {
            RegistrarLog($"ERRO inesperado ao registrar TLX: {ex.Message}");
            return (null, $"Erro inesperado: {ex.Message}");
        }
    }

    /// <summary>
    /// Determina a fase circadiana atual com base na hora do sistema.
    /// Usa switch para alterar comportamento.
    /// </summary>
    public static FaseCircadiana ObterFaseCircadianaAtual()
    {
        int hora = DateTime.Now.Hour;
        return hora switch
        {
            >= 8 and < 12  => FaseCircadiana.FocoIntenso,
            >= 12 and < 14 => FaseCircadiana.Transicao,
            >= 14 and < 18 => FaseCircadiana.FocoModerado,
            _              => FaseCircadiana.Descanso
        };
    }

    /// <summary>
    /// Retorna informações de CoordenadasTempo para o momento atual.
    /// </summary>
    public static CoordenadasTempo ObterCoordenadasAtuais(TimeSpan tempoEmTela)
    {
        var fase = ObterFaseCircadianaAtual();
        double nivelLuz = fase switch
        {
            FaseCircadiana.FocoIntenso  => 0.8,
            FaseCircadiana.Transicao    => 0.6,
            FaseCircadiana.FocoModerado => 0.5,
            FaseCircadiana.Descanso     => 0.2,
            _ => 0.5
        };

        return new CoordenadasTempo(tempoEmTela, nivelLuz, DateTime.Now);
    }
}
