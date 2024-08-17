using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestFullWebApi.Abstractions;
using RestFullWebApi.Abstractions.IRepositories;
using RestFullWebApi.Abstractions.IRepositories.ISchoolRepositories;
using RestFullWebApi.Abstractions.IRepositories.IStudentRepositories;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.Abstractions.Services;
using RestFullWebApi.Context;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Extentions;
using RestFullWebApi.Implementation.Repositories;
using RestFullWebApi.Implementation.Repositories.EntityRepositories;
using RestFullWebApi.Implementation.Services;
using RestFullWebApi.Implementation.UnitOfWork;
using RestFullWebApi.Mappers;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
services.AddScoped<ISchoolService, SchoolService>();
services.AddScoped<IStudentService, StudentService>();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<ITokenHandler, RestFullWebApi.Implementation.Services.TokenHandler>();


//repositories
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<ISchoolRepository, SchoolRepository>();
services.AddScoped<IStudentRepository, StudentRepository>();


services.AddDbContext<AppDBContext>(op =>
{
	op.UseSqlServer(builder.Configuration.GetConnectionString("MentorSchoolDB"));
});

services.AddIdentity<AppUser, AppRole>(options =>
{

}).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();


services.AddAutoMapper(typeof(MapProfile));



builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new()
	{
		ValidateAudience = true,//tokunumuzu kim/hansi origin islede biler
		ValidateIssuer = true, //tokunu kim palylayir
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true, //tokenin ozel keyi

		ValidAudience = builder.Configuration["Token:Audience"],

		ValidIssuer = builder.Configuration["Token:Issuer"],

		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

		//token omru qeder islemesi ucun
		LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

		NameClaimType = ClaimTypes.Name,
		RoleClaimType = ClaimTypes.Role
	};
});

builder.Services.AddSwaggerGen(swagger =>
{
	//This is to generate the Default UI of Swagger Documentation  
	swagger.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Restaurant Final API",
		Description = "ASP.NET Core 6 Web API"
	});
	// To Enable authorization using Swagger (JWT)  
	swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}

					}
				});
});






var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ConfigureExecptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
