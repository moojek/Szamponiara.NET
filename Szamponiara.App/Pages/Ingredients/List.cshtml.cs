#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Szamponiara.App.Authorization;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    [AllowAnonymous]
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ListModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // QA: which collection/interface to use
        public IList<Ingredient> Ingredients { get; set; }

        public async Task OnGetAsync()
        {
            // Ingredients = await _context.Ingredients.ToListAsync();
            // Ingredients.Sort((i1, i2) => string.Compare(i1.Name, i2.Name, StringComparison.InvariantCultureIgnoreCase));

            var ingredients = from i in _context.Ingredients.AsAsyncEnumerable()
                select i;

            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = User.IsInRole(Roles.IngredientsAdmin) || User.IsInRole(Roles.IngredientsModerator);
            if (!isAuthorized)
            {
                ingredients =
                    ingredients.Where(i => i.Status == IngredientStatus.Approved || i.OwnerId == currentUserId);
            }

            Ingredients = await ingredients.OrderBy(i => i.Name, StringComparer.InvariantCultureIgnoreCase)
                .ToListAsync();
        }
    }
}