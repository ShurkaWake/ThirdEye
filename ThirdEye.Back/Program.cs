using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ThirdEye.Back.DataAccess.Contexts;
using ThirdEye.Back.DataAccess.Entities;
using ThirdEye.Back.Extensions;
using ThirdEye.Back.Mapping;
using ThirdEye.Back.Services;
using ThirdEye.Back.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddTransient<IEmailSender, EmailSender>();

services.AddControllersWithViews().AddDataAnnotationsLocalization();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMapper());
});
services.AddSingleton(mapperConfig.CreateMapper() as IMapper);

services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

string connectionString = builder.Configuration["DbConnectionString"];

services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(connectionString, options =>
    {
        options.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
    });
});

services.AddControllers();

services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddUkrainianIdentityErrorDescriber()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("uk")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions() { MinimumSameSitePolicy = SameSiteMode.Lax });

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("uk"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();