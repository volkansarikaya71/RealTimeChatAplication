using ChatApi.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SignalR;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Standart servis eklemeleri
builder.Services.AddLogging();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// HttpClient ekleniyor
builder.Services.AddHttpClient();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "http://localhost",
        ValidAudience = "http://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PhoneNumberAndPasswordControlJwt")), //32
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(origin => true); // T�m k�kenlere izin ver
    });
});



var app = builder.Build();

// API controller'lar�n� haritalamak i�in MapControllers kullan�l�r
app.MapControllers();

// Swagger middleware'i ekle
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApi v1");
        c.RoutePrefix = string.Empty; // Swagger ana sayfa olarak a��ls�n
    });
}

app.UseCors(); // CORS'u burada etkinle�tir

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // 30 g�nl�k HSTS varsay�lan
}

// HTTPS y�nlendirmesi ve yetkilendirme i�lemleri
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// SignalR hub'�n� tan�ml�yoruz
app.MapHub<Login>("/login");

// Uygulaman�n �al��mas�n� ba�lat
app.Run();
