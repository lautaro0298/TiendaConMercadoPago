
using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppStore.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly IProductoService ProductoService;
        private readonly IFileService fileService;
        private readonly ICategoriaService categoriaService;

        public ProductoController(IProductoService ProductoService, IFileService fileService, ICategoriaService categoriaService)
        {
            this.ProductoService = ProductoService;
            this.fileService = fileService;
            this.categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult add(Producto Producto)
        {
            Producto.categoriasList = categoriaService
            .List().Select(a => new SelectListItem { Text = a.Nombre, Value = a.id.ToString() });
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
                return View(Producto);
            }
            if (Producto.ImagenFile != null)
            {
                var resultado = fileService.SaveImage(Producto.ImagenFile);
                if (resultado.Item1 == 0)
                {
                    TempData["Msg"] = "La imagen no puedo ser guardada exitosamente ";
                    return View(Producto);
                }
                var ImagenName = resultado.Item2;
                Producto.Imagen = ImagenName;

            }
            var resultadoProducto = ProductoService.Add(Producto);
            if (resultadoProducto)
            {
                TempData["Msg"] = "Se agrego correctamente";
                return RedirectToAction(nameof(Add));

            }
            TempData["Msg"] = "Errores guardando el Producto";
            return View(Producto);
        }
        public IActionResult Add()
        {

            var Producto = new Producto();
            Producto.categoriasList = categoriaService.List()
                .Select(a => new SelectListItem { Text = a.Nombre, Value = a.id.ToString() });

            return View(Producto);
        }
        public IActionResult Edit(int id)

        {
            var Producto = ProductoService.GetById(id);
            var categoriaDeProducto = ProductoService.GetCategoriaByProductoId(id);
            var MultiSelectListCategorias = new MultiSelectList(categoriaService.List(), "id", "Nombre", categoriaDeProducto);
            Producto.MultiCategoriaList = MultiSelectListCategorias;

            return View(Producto);
        }
        [HttpPost]
        public IActionResult Edit(Producto Producto)
        {
            var categoriaDeProducto = ProductoService.GetCategoriaByProductoId(Producto.id);
            var MultiSelectListCategorias = new MultiSelectList(categoriaService.List(), "id", "Nombre", categoriaDeProducto);
            Producto.MultiCategoriaList = MultiSelectListCategorias;
            if (!ModelState.IsValid)
            {
                return View(Producto);
            }
            if (Producto.ImagenFile != null)
            {
                var fileResultado = fileService.SaveImage(Producto.ImagenFile);
                if (fileResultado.Item1 == 0)
                {
                    TempData["Msg"] = "La imagen no puedo ser guardada exitosamente ";
                    return View(Producto);
                }
                var ImagenName = fileResultado.Item2;
                Producto.Imagen = ImagenName;

            }
            var resultadoProducto = ProductoService.Update(Producto);
            if (!resultadoProducto)
            {
                TempData["Msg"] = "Errores guardando el Producto";
                return View(Producto);

            }
            TempData["Msg"] = "Se actualizo exitosamante el Producto";
            return View(Producto);

        }
        public IActionResult ProductoList()
        {
            var Productos = ProductoService.List();
            return View(Productos);
        }
        public IActionResult Delete(int id)
        {
            ProductoService.Delete(id);
            return RedirectToAction(nameof(ProductoList));
        }
    }
}