namespace AstroHabitsDesktop.Domain.Enums;

/// <summary>
/// Representa as fases do ciclo circadiano do astronauta,
/// influenciando a interface e recomendações do sistema.
/// </summary>
public enum FaseCircadiana
{
    FocoIntenso,   // 08:00–12:00
    Transicao,     // 12:00–14:00
    FocoModerado,  // 14:00–18:00
    Descanso       // 18:00–08:00
}
