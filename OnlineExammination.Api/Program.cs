using OnlineExammination.Application;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Infrastructure;
using OnlineExammination.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration).
    AddApplicationServices(builder.Environment.WebRootPath).
    AddInfrastructureServices(builder.Configuration);
var app = builder.Build();

app.UseCors(option =>
{
    option.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
