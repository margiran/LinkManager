using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinkManagerClientWPF.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public virtual string WindowTitle => string.Empty;

    protected void Notify([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}