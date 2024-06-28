namespace Sutom.Application.Interfaces
{
    public interface IWordDataSource
    {
        Task<IEnumerable<string>> GetRandomWordsAsync();
    }
}
