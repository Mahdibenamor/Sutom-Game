using Sutom.Mobile.Core;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile.Pages
{
    public interface IBasePage<out ViewModel> where ViewModel : class, IBaseViewModel
    {
    }
}