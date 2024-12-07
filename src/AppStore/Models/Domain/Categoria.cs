using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Models.Domain
{
    public class Categoria
    {
        [Key]
        [Required]
        public int id{get;set;}
        public string? Nombre{get;set;}
          public virtual ICollection<Producto> productos {get;set;}
            public virtual ICollection<ProductoCategoria> ProductoCategorias {get;set;}
    }
}