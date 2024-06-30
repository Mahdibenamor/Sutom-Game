
namespace Sutom.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ViewModel : Attribute
    {
        public Type ViewModelType { get; }

        public ViewModel(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
