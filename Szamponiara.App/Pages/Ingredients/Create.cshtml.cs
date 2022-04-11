#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlHelper htmlHelper;
        private readonly UserManager<IdentityUser> _userManager;

        public IEnumerable<SelectListItem> Effects { get; set; }

        public CreateModel(ApplicationDbContext context, IHtmlHelper htmlHelper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
            _userManager = userManager;
            Effects = htmlHelper.GetEnumSelectList<Effect>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public Ingredient Ingredient { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Ingredient.OwnerId = _userManager.GetUserId(User);

            // TODO: initialize Ingredient.Status with relevance to user's role

            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}