using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using ProgettoS5L5SabrinaCinque.Services;
using ProgettoS5L5SabrinaCinque.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurazione dell'autenticazione
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
    });

// Configurazione delle policy di autorizzazione
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ComandantePolicy", policy => policy.RequireRole("Comandante"));
    options.AddPolicy("SubordinatoPolicy", policy => policy.RequireRole("Comandante","Subordinato"));
});

// Configurazione del servizio di gestione delle autenticazioni
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrazione dei DAO con stringa di connessione
var connectionString = builder.Configuration.GetConnectionString("PoliziaMunicipale");
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
