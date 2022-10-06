namespace DuplicateGroupTester;

internal class Program
{
    private const string uri = "okta url without https://";
    private const string apiKey = "api  token";

    static async Task Main(string[] args)
    {
        var testGroupName = "Test for duplicate groups group";
        var callsToCreateGroup = 2;
        var delayBetweenCallsMS = 50;

        var groupCheckService = new GroupCheckService(
            uri,
            apiKey);

        Console.WriteLine("Creating groups");
        await groupCheckService.CreateGroupsWithDelayInMS(
            testGroupName, 
            callsToCreateGroup, 
            delayBetweenCallsMS);

        Console.WriteLine("Checking for duplicate groups");
        foreach (var group in await groupCheckService.GetGroupsByName(testGroupName))
        {
            Console.WriteLine($"Found: {group.Profile.Name} - {group.Id} - {group.Created}");
        }
    }

}