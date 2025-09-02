using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MINTHOTEL.Models.Db;
using MINTHOTEL.Services.Interfaces;
using MINTHOTEL.Services.servicios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MintHotelsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<Iverclientes, verclientesServis>();
builder.Services.AddScoped<ISaveHotel, SaveHotelServis>();
builder.Services.AddScoped<ISavePropietario, SavePropietarioServis>();
builder.Services.AddScoped<IAccess, AccesServis>();
builder.Services.AddScoped<ISaveUser, SaveUsuarioServis>();
builder.Services.AddScoped<IVista, VisitaServis>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.AccessDeniedPath = "/Home/Privacy";
        options.SlidingExpiration = true;
        options.LogoutPath = "/Inicio/Logout";
    });

builder.Services.AddAuthorization();

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
    pattern: "{controller=Inicio}/{action=Login}/{id?}");

app.Run();