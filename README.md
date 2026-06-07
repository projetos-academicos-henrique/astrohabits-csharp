# AstroHabits - Hábitos Espaciais para a Vida na Terra 🚀

Com a ideia de trazer soluções "espaciais" para o mundo comum, o AstroHabits não busca uma tecnologia de foguete que pode ser usada num carro — mas sim **como as pessoas se tornam capazes de se manter em si mesmas fora do nosso planeta**. Como fazer uma "super" pessoa?

Este projeto traz **3 soluções inspiradas na vida no espaço** que podem ser usadas como estratégias para ter mais foco e uma vida mais leve.

## As 3 Soluções

### 1. NASA Task Load Index (TLX)
Ferramenta de avaliação multidimensional usada pela NASA para medir a carga de trabalho percebida. Avalia 6 dimensões:
- **Demanda Mental** — Quão exigente mentalmente foi a tarefa?
- **Demanda Física** — Quão exigente fisicamente?
- **Demanda Temporal** — Quanta pressão de tempo?
- **Performance** — Como você avalia seu desempenho?
- **Esforço** — Quanto esforço foi necessário?
- **Frustração** — Quão frustrante foi?

O programa exibe **gráficos de barras coloridas** e calcula um **score ponderado** com classificação (Baixa/Moderada/Alta/Crítica).

### 🛰️ 2. Modos de Órbita — Fuga do Imediatismo
No espaço, mensagens demoram para chegar. Uma comunicação Terra↔Marte leva ~20 minutos. Isso parece ruim, mas **não ter respostas na hora é um exercício de paciência**.

Escolhendo um modo de órbita, suas "notificações" são retidas por um tempo:
| Modo      | Delay       | Inspiração                        |
|-----------|-------------|-----------------------------------|
| Terra  | 0 minutos   | Comunicação instantânea           |
| Lua    | ~8 minutos  | Delay real Terra↔Lua              |
| Marte  | ~20 minutos | Delay real Terra↔Marte            |

O foco é na **tarefa atual**, sem esperar que tudo chegue em segundos.

### 3. Ciclo Circadiano — Qualidade do Sono
Astronautas na ISS veem 16 nascer-do-sol por dia. Sem controle da luz, o ritmo circadiano colapsa. Técnicas para diminuir a luz azul e melhorar a qualidade do sono são **essenciais no espaço** — e você pode usar as mesmas técnicas aqui na Terra.

O programa:
- Analisa a **fase atual do dia** e dá dicas personalizadas
- Calcula o horário ideal para **parar de usar telas** (2h antes de dormir)
- Mostra uma **simulação visual** da transição dia→noite
- Registra a qualidade do sono para acompanhamento


## Prints

![alt text](/prints/print1.png)
![alt text](/prints/print2.png)
![alt text](/prints/print3.png)
![alt text](/prints/print4.png)

## Conceitos Aplicados

Este código atende a todos os requisitos solicitados:

1. **Manipulação de arquivo como "banco de dados"**:
   - Utilizado `System.Text.Json` e `File` (`System.IO`) em `Model/TaskLoadIndex.Data.cs` e `Model/SleepEntry.Data.cs`.
   - Os arquivos `dados.json` (tarefas TLX) e `sono.json` (registros de sono) são lidos ao abrir e atualizados ao salvar.

2. **Classes Públicas, Estáticas, Privadas, Herança e Polimorfismo**:
   - `TaskLoadIndex` e `SleepEntry` herdam da classe abstrata `Entry` **(Herança)**.
   - `Entry` não pode ser instanciada diretamente e possui construtor `protected`.
   - `VerifyNumber`, `Timestamp`, `ConsoleUI` e `SleepCalculator` são classes **estáticas** (`static class`).
   - **Polimorfismo**: O método `GetDetails()` é abstrato em `Entry` e cada classe filha faz seu próprio `override`.

3. **Modularidade**:
   - Organizado em namespaces (`astroHabitsCsharp.model`, `astroHabitsCsharp.util`, `astroHabitsCsharp.Exceptions`).
   - Separação clara em pastas: Model, Util, Exceptions.

4. **Captura de Erros (`try-catch`)**:
   - `FormatException` nativa quando o usuário digita texto ao invés de número.
   - `InvalidNumberRangeException` customizada para notas fora de 1-10.
   - `InvalidTimeException` customizada para horários inválidos no módulo de sono.

5. **Structs e Partial Classes**:
   - **Structs**: `OrbitData` expandida com dados de órbita reais, delays e instâncias pré-definidas estáticas.
   - **Partial**: `TaskLoadIndex` e `SleepEntry` divididas em arquivos separados (lógica + dados).

## Estrutura de Arquivos

- Program.cs
- Exceptions
- Model
- Util

