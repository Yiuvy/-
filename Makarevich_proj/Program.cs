using System.Security.Claims;
using Makarevich_proj.Data;
using Makarevich_proj.Services;
using Makarevich_proj.Services.contracts_interfaces_;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args); //�������� ���������� ����������

// Add services to the container.
//��������� ������ ����������� � ���� ������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//�������������� �������� ���� ������ ApplicationDbContext, ������� ���������� SQL Server � ��������� ������� �����������.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//��������� ���������� ��������� ��������� ������ ���� ������ �� ����� ����������.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<ICategoryService, MemoryCategoryService>(); //зарегистрировали сервис
builder.Services.AddTransient<IProductService, MemoryProductService>(); //зарегистрировали сервис


//������������� ������� ����� � �������������� ����������� ������������� (IdentityUser).
//��������� ������������� ������� ������ ��� �����.
//����� �����������, ��� ������ ������������� �������� � ���� ������ ����� ApplicationDbContext.
builder.Services.AddDefaultIdentity<AppUser>(options =>
{options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}
)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", p =>
    p.RequireClaim(ClaimTypes.Role, "admin"));
});
builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();//Имитация отправки подтверждающего сообщения

//��������� MVC � Razor Pages ��� ����������� �������.
builder.Services.AddControllersWithViews();

//�������� ��������� ������� ����������.
var app = builder.Build();

// Configure the HTTP request pipeline.
//������������ HTTP-��������� � ��������� ��� ������ ������
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // ��� ��������� �������� � ������ ����������
}
else
{
    app.UseExceptionHandler("/Home/Error"); // ��������� ������ ��� ����������
    app.UseHsts(); // �������� HSTS � ������ �� ��������� ����
}

app.UseHttpsRedirection();   // �������������� HTTP �� HTTPS
app.UseStaticFiles();        // ������������ ����������� ������ (CSS, JS, �����������)
app.UseRouting();            // �������� �������������
app.UseAuthentication();
app.UseAuthorization();      // �������� �����������

//�������� �������� ��� MVC � Razor Pages (��� �������������).
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//await DbInit.SetupIdentityAdmin(app);


app.Run();//��������� ������ � ������� ����������������.
