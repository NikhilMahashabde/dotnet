using ShopMS.Web;
using ShopMS.Web.Services;
using ShopMS.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductService>();


var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
SD.ProductApiBase = config.GetSection("ServiceUrls")["ProductAPI"];
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

})
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = config.GetSection("ServiceUrls")["IdentityAPI"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "shopms";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("shopms");
        options.SaveTokens = true;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
