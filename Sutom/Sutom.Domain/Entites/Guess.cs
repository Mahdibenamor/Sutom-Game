using Sutom.Application.Models;

namespace Sutom.Domain.Entites
{
    public class Guess
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string PlayerGuess  { get; set; } = string.Empty;
        public DateTime GuessTime { get; set; }
        public GuessResult GuessResult { get; set; } = new GuessResult();
    }
}
