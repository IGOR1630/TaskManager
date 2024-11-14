using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Service;

namespace TaskManagerWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
     
        builder.Services.AddControllersWithViews();

        // Adiciona o AutoMapper

        // Configurar a string de conexão e o DbContext
        var connectionString = builder.Configuration.GetConnectionString("TaskManagerDatabase")
                          ?? throw new InvalidOperationException("Connection string 'TaskManagerDatabase' not found.");

        builder.Services.AddDbContext<TaskManagerContext>(options =>
        {
            options.UseMySQL(connectionString);
        });

        // Registra o serviço ITarefaService e sua implementação
        builder.Services.AddTransient<ITarefaService, TarefaService>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
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
    }
}