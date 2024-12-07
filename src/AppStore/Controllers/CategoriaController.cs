
using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers
{
    public class CategoriaController:Controller
    {
        private readonly ICategoriaService categoriaService;
 
          public CategoriaController( ICategoriaService categoriaService)
        {

            this.categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult add(Categoria categoria)
        {
            
            var mod = ModelState.IsValid;
            if (!ModelState.IsValid)
            {
                // Obtener una lista de errores de validación
                var errores = ModelState.Values.SelectMany(v => v.Errors);

                // Puedes imprimir los errores en la consola para depuración
                foreach (var error in errores)
                {
                    Console.WriteLine(error.ErrorMessage); // Muestra el mensaje de error
                }
                return View(categoria);
            }
           
            var resultadocategoria = categoriaService.Add(categoria);
            if (resultadocategoria)
            {
                TempData["Msg"] = "Se agrego correctamente";
                return RedirectToAction(nameof(Add));

            }
            TempData["Msg"] = "Errores guardando el categoria";
            return View(categoria);
        }
        public IActionResult Add()
        {

            var categoria = new Categoria();
            
            return View(categoria);
        }
        public IActionResult Edit(int id)

        {
            var categoria = categoriaService.GetById(id);
        
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
           
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
          
            var resultadocategoria = categoriaService.Update(categoria);
            if (!resultadocategoria)
            {
                TempData["Msg"] = "Errores guardando el categoria";
                return View(categoria);

            }
            TempData["Msg"] = "Se actualizo exitosamante el categoria";
            return View(categoria);

        }
        public IActionResult CategoriaList()
        {
            var categoria = categoriaService.Lista();
            return View(categoria);
        }
        public IActionResult Delete(int id)
        {
            categoriaService.Delete(id);
            return RedirectToAction(nameof(CategoriaList));
        }
    }
}