namespace Sutom.Absrtractions
{
    public interface IWordLocalDataSource : IWordDataSource
    {
        Task SaveWords(IEnumerable<string> words);
        Task<IEnumerable<string>> GetRandomWordsForLength(int length);
        Task<string> GetRandomWordForLength(int length);

    }
}
