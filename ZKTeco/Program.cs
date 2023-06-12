using Microsoft.EntityFrameworkCore;
using ZKTeco.Models;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Features;
//using FluentAssertions.Common;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10000);
});
//builder.Services.AddMvc();
builder.Services.Configure<FormOptions>(x =>
{
    x.BufferBody = false;
    x.KeyLengthLimit = int.MaxValue; // 2 KiB
    x.ValueLengthLimit = int.MaxValue; // 32 MiB
    x.ValueCountLimit = int.MaxValue;// 1024
    x.MultipartHeadersCountLimit = int.MaxValue; // 16
    x.MultipartHeadersLengthLimit = int.MaxValue; // 16384
    x.MultipartBoundaryLengthLimit = int.MaxValue; // 128
    x.MultipartBodyLengthLimit = int.MaxValue; // 128 MiB
});
builder.Services.AddMvc(options =>
{
    options.MaxModelBindingCollectionSize = int.MaxValue;
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ZKTDbContext>(options =>
                options.UseSqlServer(connectionString)); ;

var app = builder.Build();

app.UseSession();
//app.UseMvc();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
