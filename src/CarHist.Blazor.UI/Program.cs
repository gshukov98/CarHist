using CarHist.Blazor.UI.Data;
using CarHist.Blazor.UI.Services;
using Elders.Cronus;
using MatBlazor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
 {
     options.Password.RequireDigit = false;
     options.Password.RequireLowercase = false;
     options.Password.RequiredLength = 5;
     options.Password.RequireUppercase = false;
     options.Password.RequireNonAlphanumeric = false;
     options.SignIn.RequireConfirmedEmail = false;
 })
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddCronus(builder.Configuration);
builder.Services.AddMatBlazor();
builder.Services.AddTransient<CarsProvider>();
builder.Services.AddTransient<CarHistoryProvider>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() == false)
{
    app.UseDeveloperExceptionPage();
}
else
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
