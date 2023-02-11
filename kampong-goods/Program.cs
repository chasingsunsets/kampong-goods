using kampong_goods;
using kampong_goods.Hubss;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//SendGrid API
builder.Services.AddTransient<IMailService, SendGridMailService>();

//AppUser
builder.Services.AddDbContext<AppUsersDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppUsersDbContext>().AddDefaultTokenProviders(); 
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<StaffService>();
/*builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
});*/

/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeStaff",
    policy => policy.RequireClaim("Staff", "HR"));
});*/

//product
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ConditionService>();
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddDbContext<ProductDbContext>();

//chat
builder.Services.AddSignalR();

//faq
builder.Services.AddScoped<FAQCatService>();
builder.Services.AddScoped<FAQService>();
builder.Services.AddDbContext<FAQDbContext>();

//request
builder.Services.AddScoped<RequestService>();

//voucher
builder.Services.AddScoped<VoucherService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<VoucherService>();
builder.Services.AddDbContext<VoucherDbContext>();

/*//configs
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Customers/Login";
    Config.LoginPath=
    Config.AccessDeniedPath = "/Account/AccessDenied";

});
*/

//auth access denied
/*builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options
=>
{
    options.Cookie.Name = "MyCookieAuth";
    options.AccessDeniedPath = "/Account/AccessDenied";

});*/

//two login paths
builder.Services.AddAuthentication(opt => { opt.DefaultScheme = "AdminAuth"; })
    .AddCookie("UserAuth", opt =>
    {
        opt.LoginPath = "/Customers/Login";
        opt.AccessDeniedPath = "/Account/AccessDenied/";
    })
    .AddCookie("AdminAuth", opt =>
    {
        opt.LoginPath = "/Staff/StaffLogin";
        opt.AccessDeniedPath = "/Account/AccessDenied/";
    });

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // enables immediate logout, after updating the user's security stamp.
    options.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<StaffService>();

builder.Services.AddScoped<FAQService>();

//builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddDbContext<VoucherDbContext>();
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddDbContext<FAQDbContext>();
builder.Services.AddDbContext<EventDbContext>();

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

app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//end point
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/Chathub");
});

app.MapRazorPages();

app.Run();
