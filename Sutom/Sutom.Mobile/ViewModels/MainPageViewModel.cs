using Sutom.Absrtractions;
using Sutom.Domain.Entites;
using Sutom.Mobile.Services.Navigation;
using System.Windows.Input;

namespace Sutom.Mobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int wordLength;
        private int maxAttempts;
        private string wordLengthError;
        private string maxAttemptsError;

        public int WordLength
        {
            get => wordLength;
            set
            {
                wordLength = value;
                OnPropertyChanged(nameof(wordLength));
                ValidateWordLength();
            }
        }

        public int MaxAttempts
        {
            get => maxAttempts;
            set
            {
                maxAttempts = value;
                OnPropertyChanged(nameof(MaxAttempts));
                ValidateMaxAttempts();
            }
        }

        public string WordLengthError
        {
            get => wordLengthError;
            set
            {
                wordLengthError = value;
                OnPropertyChanged(nameof(WordLengthError));
            }
        }

        public string MaxAttemptsError
        {
            get => maxAttemptsError;
            set
            {
                maxAttemptsError = value;
                OnPropertyChanged(nameof(MaxAttemptsError));
            }
        }

        public ICommand BeginCommand { get; }


        public ICommand NavigateToGamePageCommand { get; }
        private readonly INavigationService _navigationService;
        private readonly IGameService _gameService;

        public MainPageViewModel(INavigationService navigation, IGameService gameService) {
            _navigationService = navigation;
            _gameService = gameService;
            BeginCommand = new Command(async () => await NavigateToGamePage());

        }

        public override async Task InitializeAsync(object parameter)
        {
            await Task.CompletedTask;
        }
        public async Task NavigateToGamePage()
        {
            ValidateWordLength();
            ValidateMaxAttempts();
            Game game = await _gameService.StartNewGameAsync(wordLenght: WordLength, attemps: MaxAttempts);
            if (string.IsNullOrEmpty(WordLengthError) && string.IsNullOrEmpty(MaxAttemptsError))
            {
                await _navigationService.NavigateToAsync<GamePageViewModel>(game);
            }
        }

        private void ValidateWordLength()
        {
            if (WordLength < 1)
            {
                WordLengthError = "Word Length must be at least 1.";
            }
            else
            {
                WordLengthError = null;
            }
        }

        private void ValidateMaxAttempts()
        {
            if (MaxAttempts < 1)
            {
                MaxAttemptsError = "Max Attempts must be at least 1.";
            }
            else
            {
                MaxAttemptsError = null;
            }
        }

       
    }
}
