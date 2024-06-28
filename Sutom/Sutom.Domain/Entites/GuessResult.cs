namespace Sutom.Application.Models
{
    public class LetterResult
    {
        public char Letter { get; set; }
        public string Status { get; set; } = string.Empty;
    }


    public class GuessResult
    {
        public bool Correct { get; set; }
        public bool ShowInfoMessage { get; set; }
        public List<LetterResult> LetterResults { get; set; } = new List<LetterResult>();

    }
}
