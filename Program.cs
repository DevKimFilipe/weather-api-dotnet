var builder = WebApplication.CreateBuilder(args);

// --- 1. CAMADA DE CONFIGURAÇÃO (BLINDAGEM) ---
var config = builder.Configuration.GetSection("WeatherSettings");
string apiKey = config["ApiKey"] ?? "Sem Chave";
string baseUrl = config["BaseUrl"] ?? "https://api.open-meteo.com/v1/forecast";

// --- 2. REGISTRO DE SERVIÇOS ---
builder.Services.AddHttpClient();

var app = builder.Build();

// --- 3. ENDPOINT ESTRATÉGICO ---
app.MapGet("/clima/recife", async (IHttpClientFactory clientFactory) =>
{
    var client = clientFactory.CreateClient();
    
    // Montagem da URL usando a base do appsettings.json
    string url = $"{baseUrl}?latitude=-8.05&longitude=-34.88&current=temperature_2m";
    
    var response = await client.GetFromJsonAsync<object>(url);

    return new 
    {
        Cidade = "Recife",
        Estado = "Pernambuco",
        ChaveUtilizada = apiKey, // Prova técnica de que a leitura do JSON funcionou
        DadosClimaticos = response,
        Timestamp = DateTime.Now
    };
});

app.Run();