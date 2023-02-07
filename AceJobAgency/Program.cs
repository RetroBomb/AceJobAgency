using AceJobAgency.Model;
using Microsoft.AspNetCore.Identity;
using AceJobAgency.Services;
using Microsoft.Extensions.Options;
using AceJobAgency.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<AceJobUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
builder.Services.AddScoped<MembershipService>();
builder.Services.AddDataProtection();
builder.Services.AddTransient(typeof(GoogleCaptchaService));

builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
    Config.SlidingExpiration = true;
});

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.AccessDeniedPath = "/Account/AccessDenied";

});

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Lockout.AllowedForNewUsers = true;
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    opts.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache(); //save session in memory

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);

});



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

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
