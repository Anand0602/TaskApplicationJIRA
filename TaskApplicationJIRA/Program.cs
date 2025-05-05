using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Services;
using TaskApplicationJIRA.Services.AccountServices;
using TaskApplicationJIRA.Services.AdminServices;
using TaskApplicationJIRA.Services.DeveloperServices;
using TaskApplicationJIRA.Services.Interfaces;
using TaskApplicationJIRA.Services.ScrumMaster;
using TaskApplicationJIRA.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });


builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IPriorityService, PriorityService>();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IDeveloperService, DeveloperService>();

builder.Services.AddScoped<IScrumMasterService, ScrumMasterService>();
 

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
