

using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertarData(DatabaseContext databaseContext, UserManager<ApplicationUser> Usuariomanager,RoleManager<IdentityRole> roleManager)// esto son lo manejadores de Usuario desde mi usuario creado y roles de aspnet
        {
            if(!roleManager.Roles.Any()){
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }
            if(!Usuariomanager.Users.Any())
            {
                var usuario = new ApplicationUser{
                    Nombre="lautaro",
                    Email="lautaro@lautaro.com"
                    ,UserName="lautaro.0298"
                };
                await Usuariomanager.CreateAsync(usuario,"PassLauta1.");
                await Usuariomanager.AddToRoleAsync(usuario,"ADMIN");
            }
            if(!databaseContext.categorias!.Any()){
              await  databaseContext.categorias!.AddRangeAsync(
                new Categoria {Nombre="Ariculos"},
                new Categoria{Nombre="Auriculares"},
                new Categoria{Nombre="Libros"}
          

            );
            await databaseContext.SaveChangesAsync();
            if(!databaseContext.productos!.Any()){
                await databaseContext.productos!.AddRangeAsync(
                    new Producto{
                        titulo="El Producto de la mancha",
                        CreateDate="06/06/2012",
                        Imagen="quijote.jpg",
                        Precio=(9.99M),
                        Autor="Miguel Cervanter"
                    },
                      new Producto{
                        titulo="Harry el sucio potter",
                        CreateDate="06/08/2000",
                        Imagen="harry.jpg",
                          Precio = (9.99M),
                        Autor ="El bananero"
                    }
                );
            }
            await databaseContext.SaveChangesAsync();
            if(!databaseContext.ProductoCategorias!.Any()){
                await databaseContext.ProductoCategorias!.AddRangeAsync(
                    new ProductoCategoria {
                        CategoriaId=1,ProductoId=2
                    },
                     new ProductoCategoria {
                        CategoriaId=2,ProductoId=2
                    }
                );
            }
            await databaseContext.SaveChangesAsync();
            }
        }
    }
}