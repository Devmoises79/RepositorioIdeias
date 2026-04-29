using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Idea Idea { get; set; } = new();
    public Project? Project { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var idea = await _context.Ideas
            .Include(i => i.Project)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (idea == null)
        {
            return NotFound();
        }

        Idea = idea;
        Project = idea.Project;

        return Page();
    }

    // Handler para reverter a conversão
    public async Task<IActionResult> OnPostRevertAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var idea = await _context.Ideas
            .Include(i => i.Project)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (idea == null)
        {
            return NotFound();
        }

        if (idea.Status == IdeaStatus.Converted && idea.Project != null)
        {
            _context.Projects.Remove(idea.Project);
            idea.Status = IdeaStatus.Active;
            idea.UpdatedAt = DateTime.UtcNow;
            idea.Project = null;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "🔄 Conversão revertida! A ideia voltou ao status 'Em avaliação'.";
        }
        else
        {
            TempData["ErrorMessage"] = "❌ Não foi possível reverter a conversão.";
        }

        return RedirectToPage("./Details", new { id = idea.Id });
    }
}