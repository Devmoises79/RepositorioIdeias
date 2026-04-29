using IdeaManager.Core.Enums;

namespace IdeaManager.Core.Entities;

public class Project
{
    public Guid Id { get; set; }
    public Guid IdeaId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
    public int ProgressPercentage { get; set; }
    
    // Navegações
    public virtual Idea Idea { get; set; } = null!;
}