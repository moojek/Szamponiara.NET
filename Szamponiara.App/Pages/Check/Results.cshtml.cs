using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Check
{
    public class ResultsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ResultsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //QA: which collection to use
        public ReadOnlyCollection<Ingredient> Ingredients { get; set; }
        
        public void OnGet()
        {
            //TODO: tidy code (null-coalescing operators)
            Ingredients = JsonConvert.DeserializeObject<ReadOnlyCollection<Ingredient>>(TempData["displayData"] as string ?? string.Empty) ?? new List<Ingredient>().AsReadOnly();
        }
    }
}