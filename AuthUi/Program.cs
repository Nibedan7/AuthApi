using AuthUi.ApiHelper;
using AuthUi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<EmployeeApiHelper>();  // Add HttpClient for EmployeeApiHelper
builder.Services.AddHttpClient<UserAuthApiHelper>();
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor to access the session

builder.Services.AddScoped<EmployeeApiHelper>(serviceProvider =>
{
    var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:ApiBaseUrl");
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var jwtToken = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

    if (string.IsNullOrEmpty(jwtToken))
    {
        throw new InvalidOperationException("JWT Token is missing from session.");
    }

    return new EmployeeApiHelper(baseUrl, jwtToken);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseSession(); // Enable session

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
