using Persistencia.AppRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Frontend.Areas.Identity.Data;
// using WebApplication16.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FrontendIdentityDbContextConnection");builder.Services.AddDbContext<FrontendIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FrontendIdentityDbContext>();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddSingleton<RepositorioProducto,RepositorioProducto>();
builder.Services.AddSingleton<RepositorioPedido,RepositorioPedido>();
builder.Services.AddSingleton<RepositorioPersona,RepositorioPersona>();
builder.Services.AddSingleton<RepositorioIngrediente,RepositorioIngrediente>();
builder.Services.AddControllersWithViews();
// builder.Services.AddAuthentication()
//    .AddGoogle(options =>
//    {
//        IConfigurationSection googleAuthNSection =
//        config.GetSection("Authentication:Google");
//        options.ClientId = googleAuthNSection["ClientId"];
//        options.ClientSecret = googleAuthNSection["ClientSecret"];
//    })
//    .AddFacebook(options =>
//    {
//        IConfigurationSection FBAuthNSection =
//        config.GetSection("Authentication:FB");
//        options.ClientId = FBAuthNSection["ClientId"];
//        options.ClientSecret = FBAuthNSection["ClientSecret"];
//    })
//    .AddMicrosoftAccount(microsoftOptions =>
//    {
//        microsoftOptions.ClientId = config["Authentication:Microsoft:ClientId"];
//        microsoftOptions.ClientSecret = config["Authentication:Microsoft:ClientSecret"];
//    })
//    .AddTwitter(twitterOptions =>
//    {
//        twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
//        twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
//        twitterOptions.RetrieveUserDetails = true;
//    });


var app = builder.Build();
app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    // context.Request.PathBase = new PathString("/foo");
    return next(context);
});
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
app.UseStaticFiles(); 
  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
