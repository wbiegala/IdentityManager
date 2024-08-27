using IdentityManager.Core;
using IdentityManager.Data;
using IdentityManager.Infrastructure;
using IdentityManager.Service.Middlewares;
using Microsoft.EntityFrameworkCore;
using IdentityManager.API.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCore();

builder.Services.AddAdminApi();

builder.Services.AddDbContext<IdentityManagerContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddRepositories();

builder.Services.AddInfrastructure();

builder.Services.AddScoped<ExceptionHandlerMiddleware>();

builder.Services.AddControllers()
    .AddAdminApiEndpoints();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityManagerContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
