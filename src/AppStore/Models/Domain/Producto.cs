

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Models.Domain
{
    public class Producto
    {   
        [Key]
        [Required]
        public int id {get;set;}
        public string? titulo{get;set;}
        public Decimal Precio { get; set; }
        public string? CreateDate{get;set;}
        public string? Imagen{get;set;}
        [Required]
        public string? Autor{get;set;}
        public virtual ICollection<Categoria>? Categorias {get;set;}
        public virtual ICollection<ProductoCategoria>? ProductoCategorias {get;set;}
        [NotMapped]
        //no se toma encuenta para persistir en la base de datos
         public List<int>? categorias {get;set;}
         [NotMapped]
         public string? CategoriaNombres{get;set;}
         [NotMapped]
         public IFormFile? ImagenFile{get;set;}
         [NotMapped]
         public IEnumerable<SelectListItem>? categoriasList{get;set;}
         [NotMapped]
         public MultiSelectList? MultiCategoriaList{get;set;}

    }
}