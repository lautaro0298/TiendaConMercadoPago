using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using AppStore.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddScoped<ICategoriaService,CategoriaService>();
//agrego servicio Authentication
builder.Services.AddScoped<IUserAuthenticateService,UserAuthenticateService>();
//agregar los servicio de Producto

builder.Services.AddScoped<IProductoService,ProductoService>();
/// Esto Sirve para inicializar la base de datos y  que imprima en la consola las consultas antes de enviarlas a la base de datos
builder.Services.AddDbContext<DatabaseContext>(opt=>{
    opt.LogTo(Console.WriteLine, new []{
        DbLoggerCategory.Database.Command.Name},LogLevel.Information).EnableSensitiveDataLogging();
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDatabase"));
});
//Configuro Usuario y roles 
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//habilita el login y generacion de tokes o cookes 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//esto se pone para poder hacer la insercion de la migracion 
using(var ambiente=app.Services.CreateScope())
{
    var services =ambiente.ServiceProvider;
    try{
    var context= services.GetRequiredService<DatabaseContext>();
    var usermanager=services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //(Ejecuta los archivos de migracion ya existentes)
    await context.Database.MigrateAsync();
   await LoadDatabase.InsertarData(context,usermanager,roleManager);
    }
    catch(Exception e){
        var loggin =services.GetRequiredService<ILogger<Program>>();
        loggin.LogError(e,"Ocurrio un error en la insercion de datos");
    }
}



app.Run();
