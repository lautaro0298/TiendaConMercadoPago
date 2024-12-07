using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class CategoriaListVm
    {
             public IQueryable<Categoria>? CategoriaList{get;set;}
        public int PageSize {get;set;}
        public int currentPage {get;set;}
        public int TotalPages{get;set;}
        public string? Term{get;set;}
    }
}