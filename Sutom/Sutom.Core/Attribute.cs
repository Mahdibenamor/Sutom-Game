
namespace Sutom.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ViewModelAttribute : Attribute
    {
        public Type ViewModelType { get; }

        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
