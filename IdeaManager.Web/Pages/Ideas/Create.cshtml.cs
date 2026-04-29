using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Infrastructure.Data;

namespace IdeaManager.Web.Pages.Ideas
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Idea Idea { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Idea.Id = Guid.NewGuid();
            Idea.CreatedAt = DateTime.UtcNow;
            Idea.UpdatedAt = null;

            _context.Ideas.Add(Idea);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✨ Ideia criada com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}