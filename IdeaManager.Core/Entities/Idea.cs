using IdeaManager.Core.Enums;

namespace IdeaManager.Core.Entities;

public class Idea
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IdeaStatus Status { get; set; } = IdeaStatus.Draft;
    
    // Navegação (quando a ideia vira projeto)
    public virtual Project? Project { get; set; }
}