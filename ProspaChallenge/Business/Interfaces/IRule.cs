namespace ProspaChallenge.Business.Interfaces
{
    public interface IRule<T>
    {
        bool IsQualifiedFor(T validationTarget);
        bool IsUnqualifiedFor(T validationTarget);
    }

    public interface IAsyncRule<T>
    {
        Task<bool> IsQualifiedForAsync(T validationTarget);
        Task<bool> IsUnqualifiedForAsync(T validationTarget);
    }
}
