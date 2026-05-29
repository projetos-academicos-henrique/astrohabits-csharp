using System.Collections.ObjectModel;
using AstroHabitsDesktop.Domain.Entities;
using AstroHabitsDesktop.Infrastructure.Data;

namespace AstroHabitsDesktop.Presentation.ViewModels;

public class TarefasViewModel : ViewModelBase
{
    private readonly JsonRepositorio<Tarefa> _repo;
    private string _novoTitulo = "";
    private string _novaDescricao = "";
    private int _novaPrioridade = 1;
    private Tarefa? _tarefaSelecionada;
    private string _mensagem = "";
    private bool _editando;

    public ObservableCollection<Tarefa> Tarefas { get; } = new();
    public string NovoTitulo { get => _novoTitulo; set => SetProperty(ref _novoTitulo, value); }
    public string NovaDescricao { get => _novaDescricao; set => SetProperty(ref _novaDescricao, value); }
    public int NovaPrioridade { get => _novaPrioridade; set => SetProperty(ref _novaPrioridade, value); }
    public string Mensagem { get => _mensagem; set => SetProperty(ref _mensagem, value); }
    public bool Editando { get => _editando; set => SetProperty(ref _editando, value); }

    public Tarefa? TarefaSelecionada
    {
        get => _tarefaSelecionada;
        set
        {
            if (SetProperty(ref _tarefaSelecionada, value) && value != null)
            {
                NovoTitulo = value.Titulo;
                NovaDescricao = value.Descricao;
                NovaPrioridade = value.Prioridade;
            }
        }
    }

    public RelayCommand AdicionarCommand { get; }
    public RelayCommand RemoverCommand { get; }
    public RelayCommand IniciarCommand { get; }
    public RelayCommand ConcluirCommand { get; }

    public TarefasViewModel(JsonRepositorio<Tarefa> repo)
    {
        _repo = repo;
        AdicionarCommand = new RelayCommand(Adicionar);
        RemoverCommand = new RelayCommand(Remover);
        IniciarCommand = new RelayCommand(Iniciar);
        ConcluirCommand = new RelayCommand(Concluir);
        Refresh();
    }

    public void Refresh()
    {
        Tarefas.Clear();
        foreach (var t in _repo.ObterTodos()) Tarefas.Add(t);
    }

    private void Adicionar()
    {
        if (string.IsNullOrWhiteSpace(NovoTitulo)) { Mensagem = "⚠️ Título obrigatório."; return; }
        var tarefa = new Tarefa(NovoTitulo.Trim(), NovaDescricao.Trim(), NovaPrioridade);
        _repo.Adicionar(tarefa);
        Tarefas.Add(tarefa);
        NovoTitulo = ""; NovaDescricao = ""; NovaPrioridade = 1;
        Mensagem = "✅ Tarefa adicionada!";
    }

    private void Remover()
    {
        if (TarefaSelecionada == null) { Mensagem = "⚠️ Selecione uma tarefa."; return; }
        _repo.Remover(TarefaSelecionada.Id);
        Tarefas.Remove(TarefaSelecionada);
        TarefaSelecionada = null;
        Mensagem = "🗑️ Tarefa removida.";
    }

    private void Iniciar()
    {
        if (TarefaSelecionada == null) { Mensagem = "⚠️ Selecione uma tarefa."; return; }
        TarefaSelecionada.Iniciar();
        _repo.Atualizar(TarefaSelecionada);
        Refresh();
        Mensagem = "🚀 Tarefa iniciada!";
    }

    private void Concluir()
    {
        if (TarefaSelecionada == null) { Mensagem = "⚠️ Selecione uma tarefa."; return; }
        TarefaSelecionada.Concluir();
        _repo.Atualizar(TarefaSelecionada);
        Refresh();
        var tempo = TarefaSelecionada.CalcularTempoGasto()?.ToString(@"hh\:mm\:ss") ?? "—";
        Mensagem = $"✅ Concluída! Tempo: {tempo}";
    }
}
