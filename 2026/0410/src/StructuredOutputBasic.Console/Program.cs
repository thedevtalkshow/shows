using Azure.AI.Projects;
using Microsoft.Agents.AI;
using Azure.Identity;
using System.ComponentModel;

var endpoint = "https://<your-project-name>.services.ai.azure.com/api/projects/structuredoutput";
var deployment = "<your-deployment-name>";

AIAgent agent = new AIProjectClient(new Uri(endpoint), new DefaultAzureCredential())
    .AsAIAgent(
        model: deployment,
        instructions: "You are a friendly assistant. Keep your answers brief.",
        name: "HelloAgent");

Console.WriteLine("Enter a state, province, or country to get information about its capital."); 

while (true)
{
    Console.WriteLine();
    Console.Write("Enter Location: ");

    string? location = Console.ReadLine();

    if (location is null)
    {
        Console.WriteLine("Exiting.");
        break;
    }

     location = location.Trim();

    string prompt = $"Please provide information about the capital of {location}.";

    AgentResponse response = await agent.RunAsync(prompt);

    Console.WriteLine($"Agent Response: {response.Text}");

    AgentResponse<CityInfo> structuredResponse = await agent.RunAsync<CityInfo>(prompt);

    Console.WriteLine($"Agent Structured Output: {structuredResponse.Result.Name}, Population: {structuredResponse.Result.Population}, Country: {structuredResponse.Result.Country}, Year Founded: {structuredResponse.Result.YearFounded}");
    
    Console.WriteLine($"Agent Structured Output Description: {structuredResponse.Result.Description}");

    Console.WriteLine($"Agent Structured Output Cuisine: {structuredResponse.Result.PopularCuisineExamples}");

    Console.WriteLine($"Agent Structured Output Text: {structuredResponse.Text}");
}

public class CityInfo
{
    [Description("A brief description of the city. Begin your response with 'Tell the show's audience that the capital of [location] is...'")]
    public string? Description { get; set; }
    public string? Name { get; set; }
    public int? Population { get; set; }
    public string? Country { get; set; }
    public int? YearFounded { get; set; }
    public string? PopularCuisineExamples { get; set; }
}