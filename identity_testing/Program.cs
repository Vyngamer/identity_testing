using identity_testing;
using identity_testing.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDataProtection();
builder.Services.Configure<captcha_service_keys>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<Users, IdentityRole>(opt => { opt.Lockout.AllowedForNewUsers = true; opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(25); opt.Lockout.MaxFailedAccessAttempts = 2; }).AddEntityFrameworkStores<AuthDbContext>();
/*builder.Services.AddIdentity<Users, IdentityRole>(opt =>
{
    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(25);
    opt.Lockout.MaxFailedAccessAttempts = 3;
});*/
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
/*builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
    options.SlidingExpiration = true;
});*/
/*builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(25);
});*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseAuthentication();

app.MapRazorPages();

app.Run();
