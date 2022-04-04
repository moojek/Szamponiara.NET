using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szamponiara.Core
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public HazardLevel HazardLevel { get; set; }
        public string Notes { get; set; }
    }

    public enum HazardLevel
    {
        Positive,
        Neutral,
        Negative,
        Tragic
    }
}
