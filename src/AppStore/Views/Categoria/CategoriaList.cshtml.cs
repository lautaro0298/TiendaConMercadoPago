using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppStore.Views.Categorias
{
    public class CategoriaList : PageModel
    {
        private readonly ILogger<CategoriaList> _logger;

        public CategoriaList(ILogger<CategoriaList> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}