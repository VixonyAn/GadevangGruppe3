using GadevangGruppe3Razor.Interfaces;
using GadevangGruppe3Razor.Models;
using GadevangGruppe3Razor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IBaneService, BaneService>();
builder.Services.AddScoped<IBrugerService, BrugerService>();
builder.Services.AddScoped<IHoldService, HoldService>();
builder.Services.AddScoped<IBegivenhedService, BegivenhedService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITilmeldHoldService, TilmeldHoldService>();
builder.Services.AddScoped<ITilmeldBegivenhedService, TilmeldBegivenhedService>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
