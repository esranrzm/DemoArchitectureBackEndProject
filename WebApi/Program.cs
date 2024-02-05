using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolves.Autofac;
using DataAccess.Context.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.

builder.Services.AddControllers();

//web tarayýcýsýndan gelen http requestlerinin karþýladýðýmýz web tarayýcýsýndaki izni olup olmadýðýný sorgulayan yapý
//builder.Services.AddCors(options=> {
//    options.AddPolicy("AllowOrigin",
//        builder => builder.WithOrigins("https://localhost:4200")); //istenilen siteler eklenebilir, SÝTE BAZLI KULLANIM
//}); 

builder.Services.AddCors(options=> {
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //tüm isteklere izin verildiði hali
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
