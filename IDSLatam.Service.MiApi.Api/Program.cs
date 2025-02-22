
using System.Text;
using IDSLatam.Service.MiApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureServices(builder.Configuration);
 
 //cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, 
                      builder => 
                      { 
                        builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                     });
});


builder.Services.AddAuthenticationCore(x => x.RequireAuthenticatedSignIn = true);
builder.Services.AddControllers();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();


builder.Services.AddEndpointsApiExplorer();


// builder.Services.Configure<APIIDSLatamConfig>(opts => builder.Configuration.GetSection("APIIDSLatamConfigProxis").Bind(opts));
// builder.Services.Configure<ExternosConfig>(opts => builder.Configuration.GetSection("APIIDSLatamExternosConfig").Bind(opts));



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.Api", Version = "v1" });
});




var secretKey = Encoding.UTF8.GetBytes(
               builder.Configuration.GetValue<string>("TokenSecretKey"));

builder.Services//.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenSecretKey"))),
            ValidIssuer = builder.Configuration.GetValue<string>("JwtIssuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JwtIssuer"),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddHttpClient();
// builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geonodo.API v1"));

}
app.UseCors(MyAllowSpecificOrigins);
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// app.UseHttpsRedirection();
app.Run();
 