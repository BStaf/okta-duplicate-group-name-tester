using Okta.Sdk;

namespace DuplicateGroupTester;

public class GroupCheckService
{
    private static IOktaClient oktaClient = null;

    public GroupCheckService(string oktaUrl, string oktaApiKey)
    {
        oktaClient = OktaClientFactory.Create(oktaUrl, oktaApiKey);
    }

    public async Task CreateGroupsWithDelayInMS(string groupName, int groupCnt, int delay)
    {
        var x = 0;
        var tasks = new List<Task>();
        while (groupCnt-- > 0)
        {
            tasks.Add(CreateGroupWithDelay(groupName, x));
            x += delay;
        }
        await Task.WhenAll(tasks);
    }

    public async Task<IEnumerable<IGroup>> GetGroupsByName(string groupName) =>
        await oktaClient.Groups.ListGroups(q: groupName).ToListAsync();

    private async Task CreateGroupWithDelay(string groupName, int delay)
    {
        await Task.Delay(delay);
        try
        {
            await CreateGroup(groupName);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed with "+e.Message);
        }
    }

    private async Task<IEnumerable<IGroup>> GetGroups() =>
        await oktaClient.Groups.ListGroups().ToListAsync();

    private async Task<IGroup> CreateGroup(string groupName) =>
        await oktaClient.Groups.CreateGroupAsync(new CreateGroupOptions()
        {
            Name = groupName,
            Description = "DeleteMe"
        });
}
