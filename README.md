# KaiserMVVMCore

### What is does

### Installation

### Usage

#### Normal IoC Container with constructor parameter resolution

Either create a new instance or use the `Default` one.

Example:

```csharp
namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Container.Default.Register<IOtherServiceInterface, OtherService>();
            Container.Default.Register<IServiceInterface, Service>();

            var service = Container.Default.GetInstance<IServiceInterface>();
            var otherService = Container.Default.GetInstance<IOtherServiceInterface>();
			
			// use the services
        }
    }

    public interface IServiceInterface { }
    public class Service : IServiceInterface 
    {
        public Service(IOtherServiceInterface otherService)
        { }
    }

    public interface IOtherServiceInterface { }
    public class OtherService : IOtherServiceInterface { }


}
```

#### Basic IoC Container for ViewModel Locators

You cannot create instances of the KaiserMVVM.IocContainer class. Use the static methods instead. Use the ``Register<T>()`` method to register all your 
classes / ViewModels. Then, create get-only Properties for the XAML to obtain the instances.

Example:

```csharp
public class ViewModelLocator
{
    public ViewModelLocator
    {
        IocContainer.Register<MainViewModel>();
        IocContainer.Register<LoginViewModel>();
        IocContainer.Register<SettingsViewModel>();
    }

    // use Properties, since these are bindable in XAML
    public MainViewModel MainViewModelInstance => IocContainer.GetInstance<MainViewModel>();
    public LoginViewModel LoginViewModelInstance => IocContainer.GetInstance<LoginViewModel>();
    public SettingsViewModel SettingsViewModelInstance => IocContainer.GetInstance<SettingsViewModel>();
}
```

#### ViewModelBase class

The ViewModelBase class is available to be inherited from. It offer an easy-to-use ``Set<T>`` method to set bound properties at runtime with no need
to call ``OnPropertyChanged(nameof(MyProperty))``.

Example:

```csharp
public class MainViewModel : ViewModelBase
{
    ....
}
```



### Honorable Mentions

Package icon created by [Gregor Cresnar](https://www.flaticon.com/authors/gregor-cresnar)