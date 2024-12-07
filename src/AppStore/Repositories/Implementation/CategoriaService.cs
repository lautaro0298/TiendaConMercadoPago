

using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DatabaseContext ctx;

        public CategoriaService(DatabaseContext databaseContext)
        {
            this.ctx = databaseContext;
        }

       public IQueryable<Categoria> List()
        {
           return ctx.categorias.AsQueryable();
        }
          public bool Add(Categoria categoria)
        {
            try{
                ctx.categorias!.Add(categoria);
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
            ctx.categorias.Remove(data);
            ctx.SaveChanges();
            return true;
          }
          catch (Exception e)
          {
            
            return false;
          }
        }

        public Categoria GetById(int id)
        {
            return ctx.categorias!.Find(id)!;
        }

       
    
        public bool Update(Categoria categoria)
        {
            try
            {
         
                ctx.categorias!.Update(categoria);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                
                return false;
            }
        }
          public CategoriaListVm Lista(string term = "", bool paging = false, int currentPage = 0)
        {
            var data= new CategoriaListVm();
            var list = ctx.categorias!.ToList();
            if(string.IsNullOrEmpty(term)){
                term=term.ToLower();
                list=list.Where(x=>x.Nombre!.ToLower().StartsWith(term)).ToList();

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
            
            data.CategoriaList= list.AsQueryable();

            return data;
        }

      
    }
}