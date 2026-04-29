using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas;

public class ConvertToProjectModel : PageModel
{
    private readonly AppDbContext _context;

    public ConvertToProjectModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Project Project { get; set; } = new Project();

    public Idea Idea { get; set; } = new Idea();

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var idea = await _context.Ideas
            .FirstOrDefaultAsync(i => i.Id == id);

        if (idea == null)
        {
            return NotFound();
        }

        // Verificar se a ideia já foi convertida
        if (idea.Status == IdeaStatus.Converted)
        {
            TempData["ErrorMessage"] = "❌ Esta ideia já foi convertida em projeto!";
            return RedirectToPage("./Details", new { id = idea.Id });
        }

        Idea = idea;
        
        // Pré-preencher dados do projeto com base na ideia
        Project.IdeaId = idea.Id;
        Project.Name = idea.Title ?? string.Empty;
        Project.Description = idea.Description ?? string.Empty;
        Project.StartDate = DateTime.Today;
        Project.Status = ProjectStatus.Planning;
        Project.ProgressPercentage = 0;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Validação do modelo
        if (!ModelState.IsValid)
        {
            // Recarregar a ideia para exibir no formulário
            var originalIdea = await _context.Ideas.FindAsync(Project.IdeaId);
            if (originalIdea != null)
            {
                Idea = originalIdea;
            }
            return Page();
        }

        // Validar se a ideia existe e não foi convertida
        var idea = await _context.Ideas.FindAsync(Project.IdeaId);
        if (idea == null)
        {
            TempData["ErrorMessage"] = "❌ Ideia não encontrada!";
            return RedirectToPage("./Index");
        }

        if (idea.Status == IdeaStatus.Converted)
        {
            TempData["ErrorMessage"] = "❌ Esta ideia já foi convertida em projeto!";
            return RedirectToPage("./Details", new { id = idea.Id });
        }

        // Definir ID do projeto
        Project.Id = Guid.NewGuid();
        
        // Garantir que a data de início seja válida
        if (Project.StartDate == default)
        {
            Project.StartDate = DateTime.Today;
        }

        // Garantir que os campos não sejam nulos
        if (string.IsNullOrEmpty(Project.Name))
        {
            Project.Name = idea.Title ?? "Projeto sem nome";
        }
        
        if (string.IsNullOrEmpty(Project.Description))
        {
            Project.Description = idea.Description ?? "Sem descrição";
        }

        // Adicionar o projeto
        _context.Projects.Add(Project);

        // Atualizar o status da ideia para Convertida
        idea.Status = IdeaStatus.Converted;
        idea.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"🚀 Projeto '{Project.Name}' criado com sucesso! A ideia foi convertida.";
        return RedirectToPage("./Details", new { id = Project.IdeaId });
    }
}