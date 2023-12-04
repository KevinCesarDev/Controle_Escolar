using Controle_Escolar.Data;
using Controle_Escolar.Repository.Prof;
using Microsoft.EntityFrameworkCore;
using Controle_Escolar.Repository;
using Controle_Escolar.Repository.Turma;
using Controle_Escolar.Repository.Atividade;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultDatabase");
builder.Services.AddDbContext<BancoContext>(opt => {opt.UseMySql(mySqlConnection,ServerVersion.AutoDetect(mySqlConnection));
});

builder.Services.AddScoped<IProfRepository,ProfRepository>();
builder.Services.AddScoped<ITurmaRepository,TurmaRepository>();
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();

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
