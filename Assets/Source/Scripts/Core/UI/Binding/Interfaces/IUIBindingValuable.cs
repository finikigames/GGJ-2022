using UnityEngine.UIElements;

namespace Source.Scripts.Core.UI.Binding.Interfaces
{
    public interface IUIBindingValuable<T>
    {
        INotifyValueChanged<T> Target { get; set; }
    }
}