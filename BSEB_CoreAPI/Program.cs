using BSEB_CoreAPI.Data;
using BSEB_CoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using BSEB_CoreAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Services --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

// Register ILoginService with its concrete implementation
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---- Add CORS ----
var allowedOrigin = "http://localhost:4200"; // Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularDevClient",
        policy =>
        {
            policy.WithOrigins(allowedOrigin)
                  .AllowAnyHeader()              // allow content-type, authorization, etc.
                  .AllowAnyMethod()              // allow POST, GET, OPTIONS, etc.
                  .AllowCredentials();           // if you need cookies/auth (optional)
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: enable CORS BEFORE any middleware that might short-circuit requests (and before MapControllers)
app.UseCors("AllowAngularDevClient");

app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
