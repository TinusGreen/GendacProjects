using XLabs.Ioc;

namespace MappingApp.ViewModel
{
    public class ViewModelLocator
    {
        public static IDependencyContainer Init()
        {
            var container = new SimpleContainer();
            container.Register<IDependencyContainer>(container);
            container.Register<MainViewModel, MainViewModel>();
            container.Register<MapViewModel, MapViewModel>();
            Resolver.SetResolver(container.GetResolver());
            return container;
       }

        public static MainViewModel Main
        {
            get { return Resolver.Resolve<MainViewModel>(); }
        }

        public static MapViewModel Map
        {
            get { return Resolver.Resolve<MapViewModel>(); }
        }
    }
}