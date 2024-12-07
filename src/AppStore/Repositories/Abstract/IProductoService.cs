

using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract
{
    public interface IProductoService
    {
        bool Add(Producto Producto);
        bool Update(Producto Producto);
        Producto GetById(int id);
        bool Delete(int id);
        ProductoListVm List(string term="",bool paging=false,int currentPage=0);
        List<int> GetCategoriaByProductoId(int ProductoId);
    }
}