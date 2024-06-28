
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile.Core
{
    public class ViewModelPageMapping
    {
        private readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public ViewModelPageMapping()
        {
            _mappings = new Dictionary<Type, Type>();
        }

        public void Register(Type viewModelType, Type pageType)
        {
            _mappings.Add(viewModelType, pageType);
        }

        public Type GetPageTypeForViewModel(Type viewModelType)
        {
            _mappings.TryGetValue(viewModelType, out var pageType);
            if (pageType == null)
            {
                throw new ArgumentException("pleaes annotate you page with view model annotation");

            }
            return pageType;
        }
    }
}
