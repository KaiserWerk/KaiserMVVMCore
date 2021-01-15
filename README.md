# KaiserMVVM

### Basic IoC Container for ViewModel Locators

You cannot create instances of the KaiserMVVM.IocContainer class. Use the static methods instead.
Use the ``Register<T>()`` method to register all your classes / ViewModels.
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
	
	public MainViewModel
}
```

### Honorable Mentions

Package icon created by [Gregor Cresnar](https://www.flaticon.com/authors/gregor-cresnar)