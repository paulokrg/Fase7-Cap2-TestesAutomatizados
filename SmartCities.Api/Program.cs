var builder = WebApplication.CreateBuilder(args);

// Se voc� quiser HTTPS, deixe UseHttpsRedirection; caso contr�rio, comente
// builder.Services.AddHttpsRedirection();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 1. Health check
app.MapGet("/health", () =>
    Results.Json(new { status = "UP" })
);

// Dados de exemplo para cidades
var cities = new[]
{
    new { id = 1, name = "S�o Paulo" },
    new { id = 2, name = "Rio de Janeiro" },
    new { id = 3, name = "Salvador"},
    new { id = 4, name = "Belo Horizonte"},
    new { id = 5, name =  "Curitiba"}
};

// 2. Listagem de todas as cidades
app.MapGet("/cities", () =>
    Results.Json(cities)
);

// 3. Buscar cidade por ID, 404 se n�o existir
app.MapGet("/cities/{id:int}", (int id) =>
{
    var city = cities.FirstOrDefault(c => c.id == id);
    return city is not null
        ? Results.Json(city)
        : Results.NotFound();
});

// dados de exemplo para acidentes
var acidentes = new[]
{
    new { id = 1, descricao = "Colis�o leve", data = "2025-05-01", local = "Av. Paulista" },
    new { id = 2, descricao = "Capotamento",    data = "2025-05-02", local = "Rua XV de Novembro" },
    new { id = 3, descricao = "Atropelamento",  data = "2025-05-03", local = "Av. Atl�ntica" }
};

// 4. Listagem de todos os acidentes
app.MapGet("/acidentes", () =>
    Results.Json(acidentes)
);

// 5. Buscar acidente por ID, 404 se n�o existir
app.MapGet("/acidentes/{id:int}", (int id) =>
{
    var acidente = acidentes.FirstOrDefault(a => a.id == id);
    return acidente is not null
        ? Results.Json(acidente)
        : Results.NotFound();
});

app.Run();
