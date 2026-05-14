using Microsoft.AspNetCore.Authentication.Cookies;
using Client.Services;
using Client.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar Autenticación por Cookies (Local al Cliente)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// Registrar servicios para interceptar peticiones y añadir el Token JWT
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtHeaderHandler>();

// Configurar HttpClient para comunicarse con la API del Servidor
builder.Services.AddHttpClient("DentisyApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5293/");
}).AddHttpMessageHandler<JwtHeaderHandler>();

// Registrar servicios personalizados
builder.Services.AddScoped<AuthService>();

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

app.UseAuthentication(); // Importante: Antes de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
