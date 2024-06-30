
namespace Sutom.Mobile.Services.DialogService
{
    public interface IDialogService
    {
        Task<bool> ConfirmationDialogAsync(string title, string message);
        Task ShowAlertAsync(string title, string message);
    }
}
