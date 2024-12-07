using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppStore.Views.Producto
{
    public class ProductoList : PageModel
    {
        private readonly ILogger<ProductoList> _logger;

        public ProductoList(ILogger<ProductoList> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}