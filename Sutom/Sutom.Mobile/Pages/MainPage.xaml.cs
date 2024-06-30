using Sutom.Mobile.Pages;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile
{
    [Sutom.Core.ViewModel(typeof(MainPageViewModel))]
    public partial class MainPage : ContentPage, IBasePage<MainPageViewModel>
    {
        private MainPageViewModel viewModel
        {
            get => BindingContext as MainPageViewModel;
            set => BindingContext = value;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
        }
    }
}
