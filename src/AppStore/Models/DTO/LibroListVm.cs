
using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class ProductoListVm
    {
        public IQueryable<Producto>? ProductoList{get;set;}
        public int PageSize {get;set;}
        public int currentPage {get;set;}
        public int TotalPages{get;set;}
        public string? Term{get;set;}
    }
}