using Contentful.Core.Configuration;
using ContentfulManagement;
using ContentfulManagement.Clients;
using ContentfulManagement.ContentTypes;
using ContentfulManagement.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using static System.Console;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Factories
        services.AddSingleton<IContentTypeFactory<BaseType>, ContentPageFactory>();

        // Clients
        services.Configure<ContentfulOptions>(context.Configuration.GetSection("Contentful"));
        services.AddHttpClient<IContentClient, ContentClient>();
    })
    .Build();

WriteLine("Welcome to the Contentful Manager!");
WriteLine("Please select an option:");
WriteLine("1. Generate random content");
WriteLine("2. Delete all content");
WriteLine("3. Generate content types");
WriteLine("4. Delete all content types");

var input = ReadKey();
if (!int.TryParse(input.KeyChar.ToString(), out var numericInput) || numericInput == (int)SelectableActions.Unknown)
{
    WriteLine("Invalid option.");
    WriteLine("Press any key to exit the application.");
    _ = ReadKey();
    Environment.Exit(0);
}

WriteLine($"\nYou have chosen option {numericInput}");
var contentClient = host.Services.GetRequiredService<IContentClient>();
switch (numericInput)
{
    case 1:
        WriteLine("Generating content...");
        break;
    case 2:
        WriteLine("Delete all content...");
        break;
    case 3:
        WriteLine("Generating content types...");
        await contentClient.CreateAndPublishContentModelsAsync();
        break;
    case 4:
        WriteLine("Deleting all content types...");
        await contentClient.ClearAllContentModelsAsync();
        break;
    default:
        WriteLine("Invalid options, press any key to exit the application.");
        ReadKey();
        Environment.Exit(0);
        break;
}

WriteLine("Done!");
WriteLine("Please press any key to exit the application");
_ = ReadKey();
Environment.Exit(0);

await host.RunAsync();