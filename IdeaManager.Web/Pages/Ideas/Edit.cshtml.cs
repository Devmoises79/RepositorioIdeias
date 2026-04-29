using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Entities;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ideaToUpdate = await _context.Ideas.FindAsync(Idea.Id);
            if (ideaToUpdate == null)
            {
                return NotFound();
            }

            ideaToUpdate.Title = Idea.Title;
            ideaToUpdate.Description = Idea.Description;
            ideaToUpdate.Category = Idea.Category;
            ideaToUpdate.Status = Idea.Status;
            ideaToUpdate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Ideia atualizada com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}