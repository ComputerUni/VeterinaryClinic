using Microsoft.AspNetCore.Authentication.Cookies;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using VeterinaryClinic.Business.Validators;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.EnableFormBindingSourceAutomaticValidation = true;
});
builder.Services.AddValidatorsFromAssemblyContaining<AnimalValidator>();

builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Animals");
    options.Conventions.AuthorizeFolder("/Appointments");
    options.Conventions.AuthorizeFolder("/Treatments");
    options.Conventions.AuthorizeFolder("/Payments");
    options.Conventions.AuthorizeFolder("/Dashboard");
    options.Conventions.AuthorizeFolder("/Manager");

    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Register");
});

builder.Services.AddRazorComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/code.html"));

app.MapRazorPages();

app.Run();