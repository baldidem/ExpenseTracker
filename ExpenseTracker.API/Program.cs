using AutoMapper;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Mapper;
using ExpenseTracker.Application.Validators;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer")));
builder.Services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperConfig())).CreateMapper());
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ExpenseCategoryCreateDtoValidator>();
//builder.Services.AddControllers()
//       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ExpenseCategoryCreateDtoValidator>());

//builder.Services.AddValidatorsFromAssemblyContaining(typeof(ExpenseCategoryCreateDtoValidator));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
