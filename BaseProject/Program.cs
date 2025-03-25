using BaseProject.Middlewares;
using Domain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Enables MVC controllers and views

// Enable localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "logs/errors-.log"),
        rollingInterval: RollingInterval.Month,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
        fileSizeLimitBytes: 10_000_000,
        retainedFileCountLimit: 31)
    .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "logs/traces-.log"),
        rollingInterval: RollingInterval.Month,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
        fileSizeLimitBytes: 10_000_000,
        retainedFileCountLimit: 31)
    .CreateLogger();

builder.Host.UseSerilog();

// Configure Entity Framework and Identity (if using Identity for authentication)
builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BaseDbContext>()
    .AddDefaultTokenProviders();

// Add authentication services
builder.Services.AddAuthentication(options => { options.DefaultScheme = IdentityConstants.ApplicationScheme; })
    .AddCookie("UserScheme", options => {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    })
    .AddCookie("AdminScheme", options => {
        options.LoginPath = "/Admin/Account/Login";
        options.LogoutPath = "/Admin/Account/Logout";
        options.AccessDeniedPath = "/Admin/Account/AccessDenied";
    });

// Add authorization services
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin").AddAuthenticationSchemes("AdminScheme"))
    .AddPolicy("UserPolicy", policy =>
        policy.RequireRole("User").AddAuthenticationSchemes("UserScheme"));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment()) {
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Logging middleware before routing
app.Use(async (context, next) => {
    Log.Information("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Correct route order
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();