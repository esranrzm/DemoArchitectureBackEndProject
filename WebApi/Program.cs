using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolves.Autofac;
using DataAccess.Context.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.

builder.Services.AddControllers();

//web taray�c�s�ndan gelen http requestlerinin kar��lad���m�z web taray�c�s�ndaki izni olup olmad���n� sorgulayan yap�
//builder.Services.AddCors(options=> {
//    options.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200")); //istenilen siteler eklenebilir, S�TE BAZLI KULLANIM
//}); 

builder.Services.AddCors(options=> {
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //t�m isteklere izin verildi�i hali
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
