namespace DDOSGuardService.Logic.Interfaces
{
    public interface ICache<T>
    {
        T this[string id] { get; }
    }
}