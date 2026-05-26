using GovUk.Frontend.AspNetCore;
using ImmForm.Web.Data;
using ImmForm.Web.Middleware;
using ImmForm.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend();
builder.Services.AddAntiforgery();
builder.Services.AddHealthChecks();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddDistributedMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<ImmFormDbContext>(options =>
        options.UseInMemoryDatabase("ImmFormDev"));
}
else
{
    builder.Services.AddDbContext<ImmFormDbContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var organisationApiBaseUrl = builder.Configuration["OrganisationApi:BaseUrl"];
if (string.IsNullOrEmpty(organisationApiBaseUrl))
{
    throw new InvalidOperationException("OrganisationApi:BaseUrl configuration is required.");
}

builder.Services.AddHttpClient<IOrganisationApiClient, OrganisationApiClient>(client =>
{
    client.BaseAddress = new Uri(organisationApiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
