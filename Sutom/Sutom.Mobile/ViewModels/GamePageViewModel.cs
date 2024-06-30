
using Sutom.Absrtractions;
using Sutom.Application.Models;
using Sutom.Domain.Entites;
using Sutom.Mobile.Services.DialogService;
using Sutom.Mobile.Services.Navigation;
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
        private ObservableCollection<ObservableCollection<Models.Cell>> board;
        public ObservableCollection<ObservableCollection<Models.Cell>> Board
        {
            get => board;
            set
            {
                board = value;
                OnPropertyChanged(nameof(Board));
            }
        }

        private Game game { get; set; }

        public int CurrentAttempt
        {
            get => currentAttempt;
            set
            {
                currentAttempt = value;
                OnPropertyChanged(nameof(CurrentAttempt));
            }
        }
        public int CurrentLetterIndex
        {
            get => currentLetterIndex;
            set
            {
                currentLetterIndex = value;
                OnPropertyChanged(nameof(CurrentLetterIndex));
            }
        }

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

        public ICommand TryAgainCommand { get; }
        public ICommand EnterCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand LetterCommand { get; }
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IGameService _gameService;


        public GamePageViewModel(INavigationService navigation, IDialogService dialogService, IGameService gameService)
        {
            _navigationService = navigation;
            _dialogService = dialogService;
            _gameService = gameService;
            TryAgainCommand = new Command(OnTryAgain);
            EnterCommand = new Command(async () => await OnEnter());
            BackCommand = new Command(OnBack);
            LetterCommand = new Command<string>(OnLetter);
            Board = new ObservableCollection<ObservableCollection<Models.Cell>>();
        }

        private void InitializeBoard()
        {

            ObservableCollection<ObservableCollection<Models.Cell>> board = new ObservableCollection<ObservableCollection<Models.Cell>>();
            for (int i = 0; i < MaxAttempts; i++)
            {
                var row = new ObservableCollection<Models.Cell>();
                for (int j = 0; j < WordLength; j++)
                {
                    row.Add(new Models.Cell { BackgroundColor = "White" });
                }
                board.Add(row);
            }
            Board = board;
            currentAttempt = 0;
            currentLetterIndex = 0;
        }

        private void OnTryAgain()
        {
            InitializeBoard();
        }

        private async Task OnEnter()
        {
            if (currentAttempt < MaxAttempts)
            {
                string guess = string.Join("", Board[currentAttempt].Select(c => c.Letter));
                GuessResult result = await _gameService.MakeGuessAsync(gameId: game.Id, guess: guess);
                updateBoardWithGuessResult(guessResult: result, currentAttempt: currentAttempt);
                await HandleGuessResulAndNavigate(guessResult: result, currentAttempt: currentAttempt, guess: guess);
                if (!result.ShowInfoMessage)
                {
                    CurrentAttempt++;
                    CurrentLetterIndex = 0;
                }

            }
        }
        private void updateBoardWithGuessResult(GuessResult guessResult, int currentAttempt)
        {
            if (!guessResult.ShowInfoMessage)
            {
                string computeCellColor(string status)
                {
                    switch (status)
                    {
                        case "correct": return "green";
                        case "misplaced": return "yellow";
                        case "wrong": return "red";
                    }
                    return "White";
                }

                ObservableCollection<ObservableCollection<Models.Cell>> board = Board;
                var row = new ObservableCollection<Models.Cell>();
                for (int i = 0; i < guessResult.LetterResults.Count; i++)
                {
                    row.Add(new Models.Cell
                    {
                        Letter = guessResult.LetterResults[i].Letter.ToString(),
                        BackgroundColor = computeCellColor(guessResult.LetterResults[i].Status)
                    });
                }
                board[currentAttempt] = row;
                Board = board;
            }
        }

        private async Task HandleGuessResulAndNavigate(GuessResult guessResult, int currentAttempt, string guess)
        {
            if (guessResult.ShowInfoMessage)
            {
                await _dialogService.ShowAlertAsync("Info", $"This word don't existe in the words dictionary: {guess}");
                return;
            }
            if (guessResult.Correct)
            {
                await _dialogService.ShowAlertAsync("Well Done", $"You have succeeded to guess the word: {guess}");
                await _navigationService.GoBackAsync();
                return;
            }
            if(currentAttempt == maxAttempts - 1) {
                await _dialogService.ShowAlertAsync("Failure", $"You have failed to guess the word");
                await _navigationService.GoBackAsync();
                return;
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
                CurrentLetterIndex++;
            }
        }

        public override async Task InitializeAsync(object parameter)
        {
            game = parameter as Game;
            MaxAttempts = game.MaxAttemps;
            WordLength = game.Difficulty;
            CurrentAttempt = 0;
            InitializeBoard();
        }
    }
}
