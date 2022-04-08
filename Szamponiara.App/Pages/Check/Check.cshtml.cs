using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Check
{
    [AllowAnonymous]
    public class CheckModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly char[] splitters = {' ', ';', ','};

        [BindProperty]
        public string? IngredientsQueryString { get; set; }

        public CheckModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            var ingredientsStrings= IngredientsQueryString?.Split(splitters);

            var queryResult = from i in _context.Ingredients.AsEnumerable()
                where (ingredientsStrings ?? Array.Empty<string>()).Any(ingredientString => string.Equals(ingredientString.Trim(), i.Name, StringComparison.CurrentCultureIgnoreCase))
                orderby i.Effect descending 
                select i;

            //QA: how to pass the data to Results page
            TempData["displayData"] = JsonConvert.SerializeObject(queryResult);

            return RedirectToPage("Results");
        }
    }
}