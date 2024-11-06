using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<ICategoryMst, CategoryMstDAL>();
builder.Services.AddScoped<ISupplierMst, SupplierMstDAL>();
builder.Services.AddScoped<IProductMst, ProductMstDAL>();

var app = builder.Build();

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
    pattern: "/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=CategoryMst}/{action=Index}/{id?}");

app.Run();
