#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Szamponiara.App.Authorization;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlHelper htmlHelper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public IEnumerable<SelectListItem> Effects { get; set; }

        public EditModel(ApplicationDbContext context, IHtmlHelper htmlHelper, UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
            _userManager = userManager;
            _authorizationService = authorizationService;
            Effects = htmlHelper.GetEnumSelectList<Effect>();
        }

        [BindProperty] public Ingredient Ingredient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ingredient = await _context.Ingredients.FirstOrDefaultAsync(m => m.Id == id);

            if (Ingredient == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Roles.IngredientsAdmin) ||
                               (Ingredient.OwnerId == _userManager.GetUserId(User) &&
                                Ingredient.Status == IngredientStatus.Submitted);

            if (!isAuthorized)
            {
                return Forbid();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ingredient = await _context.Ingredients.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Roles.IngredientsAdmin) ||
                               (ingredient.OwnerId == _userManager.GetUserId(User) &&
                                ingredient.Status == IngredientStatus.Submitted);
            if (!isAuthorized)
            {
                return Forbid();
            }

            Ingredient.OwnerId = ingredient.OwnerId;
            _context.Attach(Ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(Ingredient.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}