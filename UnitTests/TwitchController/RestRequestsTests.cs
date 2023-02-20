using Krinzh.Controllers;

namespace UnitTests;



public class RestRequestsTests
{
    private bool consoleOutput = true;
    
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public async Task AcquireAccessToken()
    // {
    //     TwitchController controller = new TwitchController();
    //     var response = Task.Run(async () => await controller.GetAccessToken());
    //     Console.WriteLine("This is Console.Writeline");
    //     string strResponse = response.Result;
    //
    //     if (consoleOutput)
    //     {
    //         Console.WriteLine("Following response was observed: \n" + strResponse);
    //     }
    //         
    //     
    //     Assert.IsNotEmpty(strResponse);
    // }
    [Test]
    public async Task GetStreamersOnline()
    {

        if (consoleOutput)
        {
            Console.WriteLine(response);
        }
            
        
        Assert.IsNotEmpty(response);
    }
}