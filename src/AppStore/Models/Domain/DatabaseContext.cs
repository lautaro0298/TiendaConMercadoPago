
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
      public DatabaseContext (DbContextOptions<DatabaseContext> options ): base(options){
        
      }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Producto>()
                .HasMany(x =>x.Categorias)
                .WithMany(x=>x.productos)
                .UsingEntity<ProductoCategoria>(
                  s => s
                  .HasOne(p=>p.categoria)
                  .WithMany(p=>p.ProductoCategorias)
                  .HasForeignKey(p=>p.CategoriaId),
                   j=>j
                  .HasOne(p=>p.Producto)
                  .WithMany(p=>p.ProductoCategorias)
                  .HasForeignKey(p=>p.ProductoId),
                  j=>{
                    j.HasKey(t=> new {t.ProductoId,t.CategoriaId});
                  }
                );
            
        }
        public DbSet<Categoria> categorias {get;set;}
        public DbSet<Producto> productos{get;set;}
        public DbSet<ProductoCategoria> ProductoCategorias {get;set;}
    }
}