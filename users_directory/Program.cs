using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using users_directory.DB;
using users_directory.Middlewares;
using users_directory.Models;
using users_directory.NewFolder;
using users_directory.Services;



var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[] { "en-US", "ka-GE", "ru-RU" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en-US")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    options.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
 builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); 
});

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseRequestLocalization(localizationOptions);
app.UseMiddleware<LocalizationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files")),
    RequestPath = "/Upload/Files"
});



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
