using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Models.Domain
{
    public class ProductoCategoria
    {
        public int id{get;set;}
        public int CategoriaId{get;set;}
        public int ProductoId{get;set;}
        public Categoria categoria{get;set;}
        public Producto? Producto{get;set;}
    }
}