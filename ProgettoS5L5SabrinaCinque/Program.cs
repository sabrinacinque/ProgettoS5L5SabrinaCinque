using Microsoft.Extensions.DependencyInjection;
using ProgettoS5L5SabrinaCinque.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurazione della stringa di connessione
var connectionString = builder.Configuration.GetConnectionString("PoliziaMunicipale");

// Registrazione dei DAO e dei servizi
builder.Services.AddScoped<IVerbaleDao>(provider => new VerbaleDao(connectionString));
builder.Services.AddScoped<IAnagraficaDao>(provider => new AnagraficaDao(connectionString));
builder.Services.AddScoped<ITipoViolazioneDao>(provider => new TipoViolazioneDao(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
