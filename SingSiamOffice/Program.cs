using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using SingSiamOffice.Authentication;
using SingSiamOffice.Data;
using SingSiamOffice.Manage;
using SingSiamOffice.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();//add
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddSingleton<SingSiamOffice.Services.ReadNatID_Service>();
builder.Services.AddSingleton<SingSiamOffice.Manage.EventLog>();
builder.Services.AddSingleton<SingSiamOffice.Manage.UserManagement>();
builder.Services.AddSingleton<SingSiamOffice.Manage.Managements>();
builder.Services.AddSingleton<SingSiamOffice.Manage.PromiseManagement>();
builder.Services.AddSingleton<SingSiamOffice.Manage.GlobalData>();
builder.Services.AddSingleton<SingSiamOffice.Manage.Collateral1>();
builder.Services.AddSingleton<SingSiamOffice.Manage.Collateral2>();
builder.Services.AddSingleton<SingSiamOffice.Manage.Collateral3>();
builder.Services.AddSingleton<SingSiamOffice.Helpers.NumberToText>();
builder.Services.AddSingleton<SingSiamOffice.Helpers.calamount>();
builder.Services.AddDbContext<SingSiamOffice.Models.SingsiamdbContext>();
builder.Services.AddScoped<UserLoginService>();
builder.Services.AddSingleton<SingSiamOffice.Manage.BranchService>();
builder.Services.AddHttpClient();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7157") });

}
else
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://141.11.33.219:8000/") });
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseCookiePolicy();
//app.UseAuthentication();
//app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapDefaultControllerRoute();//add
    endpoints.MapFallbackToPage("/_Host");
});

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.Run();


