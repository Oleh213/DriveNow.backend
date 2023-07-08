﻿using DriveNow.Context;
using DriveNow.DBContext;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
        });

        var authOptionConfiguration = builder.Configuration.GetSection("Auth");

        builder.Services.Configure<AuthOptions>(authOptionConfiguration);

        var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>(); 

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}