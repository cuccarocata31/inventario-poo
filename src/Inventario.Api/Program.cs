using Inventario.Api.GraphQL;
using Inventario.Api.Middleware;
using Inventario.Core.Interfaces;
using Inventario.Core.Services;
using Inventario.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// REST + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    o.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, "Inventario.Api.xml")));

// Manejo de errores (ProblemDetails)
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Inyección de dependencias: cada interfaz con su implementación
builder.Services.AddInventarioData(builder.Configuration.GetConnectionString("Inventario")!);
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddErrorFilter<DominioErrorFilter>();

var app = builder.Build();

// Crea/actualiza la base al arrancar (así el proyecto corre sin pasos extra)
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<InventarioDbContext>().Database.Migrate();
}

app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGraphQL();   // /graphql

app.Run();