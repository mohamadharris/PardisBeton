using BLL;
using BLL.Base;
using Common.Interfaces;
using Common.Utilities;
using DAL.dbmodel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PardisBeton.Data;
using Repository.EF.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<PardisBetonDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<PardisBetonDbContext>();
builder.Services.AddControllersWithViews();

// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // Redirect to the login page in the admin area
    options.LoginPath = new PathString("/Admin/Login");
    options.AccessDeniedPath = new PathString("/Admin/Login"); // Ensure access denied redirects to login
});

//Configure Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
{
    var context = provider.GetRequiredService<PardisBetonDbContext>();
    return new UnitOfWork(context);
});


////List of namespaces to add to service builder
//var namespacesToRegister = new List<string>()
//{
//    "Common.Utilities",
//    "BLL"
//};


//// Get current assembly
//var assembly = Assembly.GetExecutingAssembly();
//var types = assembly.GetTypes();

//foreach (var type in types)
//{
//    if (namespacesToRegister.Contains(type.Namespace) && type.IsClass && !type.IsAbstract)
//    {
//        if (typeof(ISingletonService).IsAssignableFrom(type))
//        {
//            builder.Services.AddSingleton(type);
//        }
//        else if (typeof(IScopedService).IsAssignableFrom(type))
//        {
//            builder.Services.AddScoped(type);
//        }
//        else if (typeof(ITransientService).IsAssignableFrom(type))
//        {
//            builder.Services.AddTransient(type);
//        }
//    }

//}


//Adding classes as services
//Commmon - Utilities
builder.Services.AddTransient<FileUtility>();

//BLL
builder.Services.AddTransient<BLProject>();
builder.Services.AddTransient<BLAbout>();
builder.Services.AddTransient<BLPost>();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "adminArea",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.MapControllerRoute(
    name: "title",
    pattern: "{controller}/{action=Index}/{id}/{title?}"
    ); app.MapRazorPages();

app.Run();
