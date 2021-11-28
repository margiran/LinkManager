using System.Runtime.InteropServices;
using System.ComponentModel;
using LinkManagerClientCLI.TestableOutput;
using CommandLine;
using System.Threading.Tasks;
using LinkManagerClientCLI.Services;
using LinkManagerClientCLI.Domains;
using System.Text.Json;

namespace LinkManagerClientCLI;

public class LinkManagerApiClientApplication
{
    private readonly IConsoleWriter _consoleWriter;
    private readonly ILinkServices _linkServices;

    public LinkManagerApiClientApplication(IConsoleWriter consoleWriter, ILinkServices linkServices)
    {
        _consoleWriter = consoleWriter;
        _linkServices = linkServices;
    }
    public async Task RunAsync(string[] args)
    {
        await Parser
            .Default
            .ParseArguments<LinkManagerApiClientApplicationOption>(args)
            .WithParsedAsync(option =>
           {
               var result = _linkServices.SearchLinks(new LinkSearchRequest { UpdateAt = option.UpdateAt });
               var responseText = JsonSerializer.Serialize(result, new JsonSerializerOptions
               {
                   WriteIndented = true
               });

               _consoleWriter.WriteLine($"{responseText}");
               return Task.CompletedTask;
           });


    }
}
