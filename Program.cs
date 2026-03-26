var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Nossa lista de cidades estratégica
var cidades = new[] { "Recife", "Sao Paulo", "Lisboa", "Berlim" };

app.MapGet("/clima/{cidade}", (string cidade) =>
{
    // Lógica de Gestão de Riscos: Verificar se a cidade existe no nosso radar
    if (!cidades.Contains(cidade, StringComparer.OrdinalIgnoreCase))
    {
        return Results.NotFound(new { mensagem = "Cidade fora da nossa cobertura de monitoramento." });
    }

    // Simulando dados (Em um cenário real, aqui faríamos o Fetch de uma API externa)
    var previsao = new 
    {
        Cidade = cidade,
        Temperatura = Random.Shared.Next(15, 35), // Gera uma temperatura aleatória
        Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
        Aviso = "Dados simulados para teste de performance .NET"
    };

    return Results.Ok(previsao);
});

app.Run();