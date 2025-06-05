namespace apbd12.Services;

public interface IClientService
{
    public Task DeleteClient(CancellationToken token, int id);
}