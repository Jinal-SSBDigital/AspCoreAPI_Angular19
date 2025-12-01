using BSEB_CoreAPI.Data;
using BSEB_CoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using BSEB_CoreAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);
// -------------------- Services --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
// Add services to the container.

// Register ILoginService with its concrete implementation
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
