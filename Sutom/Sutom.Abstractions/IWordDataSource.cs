namespace Sutom.Absrtractions
{
    public interface IWordDataSource
    {
        Task<IEnumerable<string>> GetRandomWordsAsync();
    }
}
