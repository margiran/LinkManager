using CommandLine;

namespace LinkManagerClientCLI
{
    public class LinkManagerApiClientApplicationOption
    {
        [Option('u',"updateAt",Required=false,HelpText="filter links updated after ")]
        public string? UpdateAt { get; init; }
        
        
    }
}