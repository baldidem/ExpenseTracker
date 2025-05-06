using ExpenseTracker.API.Middlewares;
using ExpenseTracker.API.Services;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Application.Interfaces.CurrentUser;
using ExpenseTracker.Application.Mapper;
using ExpenseTracker.Application.Services.Expense;
using ExpenseTracker.Application.Services.ExpenseCategory;
using ExpenseTracker.Application.Services.Report;
using ExpenseTracker.Application.Services.Role;
using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Application.Settings;
using ExpenseTracker.Application.Validators.ExpenseCategory;
using ExpenseTracker.Infrastructure.Auth;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Repositories;
using ExpenseTracker.Persistence.Services.Report;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//////// JWT

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ClockSkew = TimeSpan.Zero // Expiration süresinde çok küçük sapmalara izin vermez
    };
});
#region Services
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IReportService, ReportService>();
#endregion

builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<AppSaveChangesInterceptor>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expense Tracker", Version = "v1.0" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Expense Tracker",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    { securityScheme, new string[] { } }
            });
});


builder.Services.AddAuthorization();
//////// JWT
//builder.Services.AddControllers();
builder.Services.AddScoped<IBcryptPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer")));

//builder.Services.AddDbContext<ExpenseTrackerDbContext>((serviceProvider, options) =>
//{
//    var interceptor = serviceProvider.GetRequiredService<AppSaveChangesInterceptor>();
//    options
//        .UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServer"))
//        .AddInterceptors(interceptor);
//});

//builder.Services.AddSingleton(new MapperConfiguration(x => x.AddProfile(new MapperConfig())).CreateMapper());
builder.Services.AddAutoMapper(typeof(ExpenseCategoryMapper).Assembly);



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssemblyContaining<ExpenseCategoryCreateDtoValidator>();

builder.Services.AddControllers()
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ExpenseCategoryCreateDtoValidator>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
