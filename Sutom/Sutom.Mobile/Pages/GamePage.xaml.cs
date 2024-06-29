using Sutom.Core;
using Sutom.Mobile.Pages;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile
{
    [ViewModel(typeof(GamePageViewModel))]
    public partial class GamePage : ContentPage, IBasePage<GamePageViewModel>
    {

        private GamePageViewModel viewModel
        {
            get => BindingContext as GamePageViewModel;
            set => BindingContext = value;
        }

        public GamePage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            viewModel.CountViewModel++;
        }

    }
}