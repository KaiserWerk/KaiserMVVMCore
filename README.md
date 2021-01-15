# KaiserMVVMCore

### What is does

### Installation

### Usage

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
  public MainViewModel MainViewModalInstance => IocContainer.GetInstance<MainViewModel>();
  public LoginViewModel LoginViewModalInstance => IocContainer.GetInstance<LoginViewModel>();
  public SettingsViewModel SettingsViewModalInstance => IocContainer.GetInstance<SettingsViewModel>();
}
```

### Honorable Mentions

Package icon created by [Gregor Cresnar](https://www.flaticon.com/authors/gregor-cresnar)