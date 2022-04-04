﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Szamponiara.Core;
using Szamponiara.Data;

namespace Szamponiara.App.Pages.Ingredients
{
    public class ListModel : PageModel
    {
        private readonly Szamponiara.Data.ApplicationDbContext _context;

        public ListModel(Szamponiara.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // QA: which collection/interface to use
        public List<Ingredient> Ingredients { get;set; }

        public async Task OnGetAsync()
        {
            Ingredients = await _context.Ingredients.ToListAsync();
            Ingredients.Sort();
        }
    }
}
