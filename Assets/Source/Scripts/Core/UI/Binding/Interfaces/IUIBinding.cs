using UnityEngine.UIElements;

namespace Source.Scripts.Core.UI.Binding.Interfaces
{
    public interface IUIBinding<TElement> where TElement : VisualElement
    {
        TElement Target { get; set; }
    }
}