
namespace Sutom.Mobile.Services.DialogService
{
    public class DialogService : IDialogService
    {
        public async Task<bool> ConfirmationDialogAsync(string title, string message)
        {
            bool answer = await App.Page.DisplayAlert(title, message, "Yes", "No");
            return answer;
        }

        public async Task ShowAlertAsync(string title, string message)
        {
            await App.Page.DisplayAlert(title, message, "OK");
        }

    }
}
