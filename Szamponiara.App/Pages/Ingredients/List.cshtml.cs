#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    [AllowAnonymous]
    public class ListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // QA: which collection/interface to use
        public List<Ingredient> Ingredients { get; set; }

        public async Task OnGetAsync()
        {
            Ingredients = await _context.Ingredients.ToListAsync();
            Ingredients.Sort((i1, i2) => string.Compare(i1.Name, i2.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
