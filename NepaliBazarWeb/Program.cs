using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NepaliBazar.DataAccess.Data;
using NepaliBazar.DataAccess.Repository;
using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Strip"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(op =>
{
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequiredLength = 3;
    op.Password.RequireDigit = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireLowercase = false;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Admin/Account/login";
    options.LogoutPath = $"/Admin/Account/logout";
    options.AccessDeniedPath = $"/Admin/Account/AccessDenied";
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
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<String>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
