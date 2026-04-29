using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Entities;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Idea> Ideas { get; set; } = new();

    public async Task OnGetAsync()
    {
        Ideas = await _context.Ideas
            .Include(i => i.Project)  // Carrega o projeto relacionado
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }
}