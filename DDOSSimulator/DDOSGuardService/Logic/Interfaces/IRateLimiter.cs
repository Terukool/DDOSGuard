namespace DDOSGuardService.Logic.Interfaces
{
    public interface IRateLimiter
    {
        bool ShouldBlock(string id);
    }
}