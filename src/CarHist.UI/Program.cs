using CarHist.UI.Services;
using Elders.Cronus;
using MatBlazor;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCronus(builder.Configuration);
builder.Services.AddMatBlazor();
builder.Services.AddTransient<CarsProvider>();

WebApplication app = builder.Build();

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
