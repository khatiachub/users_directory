using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
    options.MapType<Gender>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(Gender))
            .Select(name => new OpenApiString(name))
            .Cast<IOpenApiAny>()
            .ToList()
    });
    options.MapType<PhoneType>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(PhoneType))
           .Select(name => new OpenApiString(name))
           .Cast<IOpenApiAny>()
           .ToList()
    });
    options.MapType<RelationshipType>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(RelationshipType))
           .Select(name => new OpenApiString(name))
           .Cast<IOpenApiAny>()
           .ToList()
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
