using KaiserMVVMCore.Exceptions;
using KaiserMVVMCore.Properties.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace KaiserMVVMCore
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Set<T>(ref T myVar, T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                myVar = value;
                OnPropertyChanged(propertyName);
            }
            else
            {
                throw new PropertyNameUnknownException();
            }
        }
    }
}
