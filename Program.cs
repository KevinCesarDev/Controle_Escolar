using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Programa.Data;
using Programa.Repository.Atividade;
using Programa.Repository.Conta;
using Programa.Repository.Nota;
using Programa.Repository.Prof;
using Programa.Repository.Turma;
using Programa_DS2.Repository.Diciplina;
using Programa_DS2.Repository.Emenda;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultDatabase");

builder.Services.AddDbContext<DataContex>(opt => {
    opt.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
});

builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITurmaRepository,TurmaRepository>();
builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddScoped<IEmentaRepository, EmentaRepository>();
builder.Services.AddScoped<INotaRepository,NotaRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
