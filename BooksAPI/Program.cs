using Books.Data;
using Books.Data.Entities;
using Books.Data.Interfaces;
using Books.Data.Repositories;
using Books.Services.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
}));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Books.Data.Entities.Book>,BooksRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorsRepository>();
builder.Services.AddScoped<BooksContext, BooksContext>();
builder.Services.AddScoped<BooksService, BooksService>();
builder.Services.AddScoped<AuthorsService, AuthorsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
