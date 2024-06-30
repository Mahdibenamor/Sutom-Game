namespace Sutom.Mobile.Models
{
    public class Cell
    {
        private string letter  ="";
        private string backgroundColor = "";

        public string Letter
        {
            get => letter;
            set
            {
                letter = value;
            }
        }

        public string BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
            }
        }
    }
}
