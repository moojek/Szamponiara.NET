#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    public class CreateModel : PageModel
    {
        private readonly Szamponiara.Data.ApplicationDbContext _context;
        private readonly IHtmlHelper htmlHelper;

        public IEnumerable<SelectListItem> Effects { get; set; }

        public CreateModel(Szamponiara.Data.ApplicationDbContext context, IHtmlHelper htmlHelper)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
            Effects = htmlHelper.GetEnumSelectList<Effect>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
