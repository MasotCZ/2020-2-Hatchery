using System.Reflection;
using CampWebAPISample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CampContext>();
builder.Services.AddScoped<ICampRepository, CampRepository>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddSwaggerDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseAuthorization();
//ui
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseReDoc();

app.MapControllers();

app.Run();