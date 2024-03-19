using HierarchicalView.Domain.Interfaces.Category;
using HierarchicalView.Domain.Interfaces.Subcategory;
using HierarchicalView.Infrastructure.Data;
using HierarchicalView.Infrastructure.Repositories;
using HierarchicalView.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectstr = builder.Configuration.GetConnectionString("MSSql");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectstr);
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // .AddRazorRuntimeCompilation() ×òîáû âåðñòêà îáíîâëÿëàñü


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();


var app = builder.Build();

 
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
