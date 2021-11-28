namespace LinkManagerClientCLI.TestableOutput
{
    public class ConsoleWriter :IConsoleWriter
    {
        public void WriteLine(string textToWrite)
        {
            Console.WriteLine(textToWrite);
        }
    }
}