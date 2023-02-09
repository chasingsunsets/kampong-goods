using kampong_goods;
using kampong_goods.Hubss;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<AppUsersDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppUsersDbContext>();

//product
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ConditionService>();
builder.Services.AddScoped<FAQCatService>();
builder.Services.AddSignalR();

/*builder.Services.AddTransient<IEmailSender, SendGridEmail>();
*/
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = "/Login";
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // enables immediate logout, after updating the user's security stamp.
    options.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<StaffService>();

builder.Services.AddScoped<FAQService>();

//builder.Services.AddDbContext<MyDbContext>();

builder.Services.AddScoped<VoucherService>();
builder.Services.AddDbContext<VoucherDbContext>();
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddDbContext<FAQDbContext>();
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

app.UseAuthentication();

app.UseAuthorization();

//end point
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/Chathub");
});

app.MapRazorPages();

app.Run();
