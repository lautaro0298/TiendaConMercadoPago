
using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class ProductoService : IProductoService
    {
        private readonly DatabaseContext ctx;
        public ProductoService(DatabaseContext ctxparam){
            ctx=ctxparam;
        }
        public bool Add(Producto Producto)
        {
            try{
                ctx.productos!.Add(Producto);
                ctx.SaveChanges();
                foreach(int categoriaId in Producto.categorias!){
                    var  ProductoCategoria = new ProductoCategoria{
                        ProductoId=Producto.id,
                        CategoriaId=categoriaId
                    };
                    ctx.ProductoCategorias!.Add(ProductoCategoria);
                    
                }
                ctx.SaveChanges();
                    return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
          try
          {
            var data = GetById(id);
            if(data==null){
                return false;
            }
            var ProductoCategoria=ctx.ProductoCategorias!.Where(a=>a.ProductoId==id);
            ctx.ProductoCategorias.RemoveRange(ProductoCategoria);
            ctx.productos.Remove(data);
            ctx.SaveChanges();
            return true;
          }
          catch (Exception e)
          {
            
            return false;
          }
        }

        public Producto GetById(int id)
        {
            return ctx.productos!.Find(id)!;
        }

        public List<int> GetCategoriaByProductoId(int ProductoId)
        {
            return ctx.ProductoCategorias!.Where(a=>a.ProductoId==ProductoId).Select(a=>a.CategoriaId).ToList();
        }

        public ProductoListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data= new ProductoListVm();
            var list = ctx.productos!.ToList();
            if(string.IsNullOrEmpty(term)){
                term=term.ToLower();
                list=list.Where(x=>x.titulo!.ToLower().StartsWith(term)).ToList();

            }
            if(paging){
                int  PageSize=5;
                int count = list.Count;
                int TotalPages= (int)Math.Ceiling(count/(double)PageSize);
                list =list.Skip((currentPage-1)*PageSize).Take(PageSize).ToList();
                data.PageSize=PageSize;
                data.currentPage=currentPage;
                data.TotalPages=TotalPages;

            }
            foreach(var Producto in list){
                var categoria = (
                    from Categoria in ctx.categorias
                    join lc in ctx.ProductoCategorias!
                    on Categoria.id equals lc.CategoriaId
                    where lc.ProductoId == Producto.id
                    select Categoria.Nombre
                ).ToList();
                string categiriaNombres = string.Join(",",categoria);
                Producto.CategoriaNombres = categiriaNombres;
            
            }
            data.ProductoList= list.AsQueryable();

            return data;
        }

        public bool Update(Producto Producto)
        {
            try
            {
                var categoriasParaEliminar = ctx.ProductoCategorias.Where(a=>a.ProductoId==Producto.id);
                foreach(var categoria in categoriasParaEliminar)
                {
                    ctx.ProductoCategorias!.Remove(categoria);
                }
                foreach (var categoriaId in Producto.categorias!)
                {
                    var ProductoCategoria = new ProductoCategoria { CategoriaId=categoriaId , ProductoId =Producto.id};
                    ctx.ProductoCategorias!.Add(ProductoCategoria);
                }
                ctx.productos!.Update(Producto);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                
                return false;
            }
        }
    }
}