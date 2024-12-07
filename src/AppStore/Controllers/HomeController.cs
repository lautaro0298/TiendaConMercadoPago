
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class HomeController: Controller
    {
        private readonly IProductoService _ProductoServis;
        public HomeController(IProductoService Producto){
            _ProductoServis=Producto;
        }
        public IActionResult Index(string term="",int currentPage=1){
            var Productos =_ProductoServis.List(term,true,currentPage);
            return View(Productos);
        }
        public IActionResult ProductoDetail(int ProductoId){
           var Producto= _ProductoServis.GetById(ProductoId);
           return View(Producto);
        }
        public IActionResult About(){
            return View();
        }
    }

}