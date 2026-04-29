using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdeaManager.Core.Entities;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Idea Idea { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Ideas.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }

            Idea = idea;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var idea = await _context.Ideas.FindAsync(Idea.Id);
            if (idea != null)
            {
                _context.Ideas.Remove(idea);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "🗑️ Ideia excluída com sucesso!";
            }

            return RedirectToPage("./Index");
        }
    }
}