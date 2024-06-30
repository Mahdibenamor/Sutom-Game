
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Sutom.Mobile.ViewModels
{
    public class GamePageViewModel : BaseViewModel
    {
        private int wordLength;
        private int maxAttempts;
        private int currentAttempt;
        private int currentLetterIndex;

        public int WordLength
        {
            get => wordLength;
            set
            {
                wordLength = value;
                OnPropertyChanged(nameof(WordLength));
                InitializeBoard();
            }
        }

        public int MaxAttempts 
        {
            get => maxAttempts;
            set
            {
                maxAttempts = value;
                OnPropertyChanged(nameof(MaxAttempts));
                InitializeBoard();
            }
        }

        public ObservableCollection<ObservableCollection<Models.Cell>> Board { get; set; }
        public ICommand TryAgainCommand { get; }
        public ICommand EnterCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand LetterCommand { get; }


        private int _countViewModel = 10;
        public int CountViewModel
        {
            get { return _countViewModel; }
            set
            {
                if (_countViewModel != value)
                {
                    _countViewModel = value;
                    OnPropertyChanged(nameof(CountViewModel));
                }
            }
        }


        public GamePageViewModel()
        {
            TryAgainCommand = new Command(OnTryAgain);
            EnterCommand = new Command(OnEnter);
            BackCommand = new Command(OnBack);
            LetterCommand = new Command<string>(OnLetter);
            Board = new ObservableCollection<ObservableCollection<Models.Cell>>();
            maxAttempts = 3;
            wordLength = 3;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            Board.Clear();
            for (int i = 0; i < MaxAttempts; i++)
            {
                var row = new ObservableCollection<Models.Cell>();
                for (int j = 0; j < WordLength; j++)
                {
                    row.Add(new Models.Cell { BackgroundColor = "White" });
                }
                Board.Add(row);
            }
            currentAttempt = 0;
            currentLetterIndex = 0;
        }

        private void OnTryAgain()
        {
            InitializeBoard();
        }

        private void OnEnter()
        {
            if (currentAttempt < MaxAttempts - 1)
            {
                currentAttempt++;
                currentLetterIndex = 0;
            }
        }

        private void OnBack()
        {
            if (currentLetterIndex > 0)
            {
                currentLetterIndex--;
                Board[currentAttempt][currentLetterIndex].Letter = "";
            }
        }

        private void OnLetter(string letter)
        {
            if (currentLetterIndex < WordLength)
            {
                Board[currentAttempt][currentLetterIndex].Letter = letter;
                currentLetterIndex++;
            }
        }


        public override async Task InitializeAsync(object parameter)
        {
            await Task.CompletedTask;
        }
    }
}
