using EventProject.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MySqlDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// Aplicar migraciones automáticamente

// Crea un "espacio temporal" para pedir servicios
using (var scope = app.Services.CreateScope())
{
    // Pide el DbContext dentro de ese espacio
    var db = scope.ServiceProvider.GetRequiredService<MySqlDbContext>();
    
    // Aplica las migraciones pendientes a la base de datos
    db.Database.Migrate();
}
// Aquí el scope se destruye automáticamente y libera memoria

app.Run();
