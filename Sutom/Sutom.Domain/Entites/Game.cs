using Sutom.Core;

namespace Sutom.Domain.Entites
{
    public class Game
    {
        public int Id { get; set; } = Utils.GenerateRandomId();
        public string Word { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<Guess> Guesses { get; set; } = [];
        public int MaxAttemps { get; set; } = 0;
        public int Difficulty { get; set; } = 0;
    }
}
