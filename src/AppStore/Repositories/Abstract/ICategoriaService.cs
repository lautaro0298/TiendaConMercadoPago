
using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract
{
    public interface ICategoriaService
    {
         bool Add(Categoria categoria);
        bool Update(Categoria categoria);
        Categoria GetById(int id);
        bool Delete(int id);
        CategoriaListVm Lista(string term="",bool paging=false,int currentPage=0);
        IQueryable<Categoria> List();
    }
}