using EventSphere.Db;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Jwt:Key"], opt =>
{
   opt.Events = new JwtBearerEvents
   {
       OnMessageReceived = ctx =>
       {
           ctx.Request.Cookies.TryGetValue(builder.Configuration["Jwt:CookieName"]!, out string? accessToken);
           if (!string.IsNullOrEmpty(accessToken)) ctx.Token = accessToken;
           return Task.CompletedTask;
       }
   };
})
.AddAuthorization()
.AddFastEndpoints()
.SwaggerDocument(opt =>
{
    opt.DocumentSettings = doc =>
    {
        doc.MarkNonNullablePropsAsRequired();
        doc.AddAuth("JwtCookie", new OpenApiSecurityScheme { Name = "accessToken", In = OpenApiSecurityApiKeyLocation.Cookie, Type = OpenApiSecuritySchemeType.ApiKey });
        doc.DocumentName = "v1";
        // doc.SchemaSettings.SchemaNameGenerator = new SchemaNameGenerator();
    };
    opt.EnableJWTBearerAuth = false;
    opt.ShortSchemaNames = true;
});

string? connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddMySql<EventSphereDbContext>(connectionString, ServerVersion.AutoDetect(connectionString));
builder.Services.AddTransient<EventSphereSeeder>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        string[]? websites = builder.Configuration.GetSection("Endpoints:Website").Get<string[]>();
        
        if (websites is not null)
        {
            policy.WithOrigins(websites).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EventSphereDbContext>();
    context.Database.EnsureCreated();
    
    var seeder = scope.ServiceProvider.GetRequiredService<EventSphereSeeder>();
    seeder.Seed();
}

app.UseDefaultFiles().UseStaticFiles();
app.MapFallbackToFile("index.html");
app.UseCors();

app.UseAuthentication().UseAuthorization();

app.UseFastEndpoints(opt =>
{
   opt.Endpoints.RoutePrefix = "api";
   opt.Endpoints.Configurator = conf => { conf.Description(desc => { desc.WithName(conf.EndpointType.Namespace!.Split('.')[^1]); }); };
   opt.Validation.EnableDataAnnotationsSupport = true;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.Run();
