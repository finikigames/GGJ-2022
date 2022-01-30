using System.Collections.Generic;
using Source.Scripts.Core.UI.Binding.Bindings;
using Source.Scripts.Core.UI.Binding.Interfaces;
using UnityEngine.UIElements;

namespace Source.Scripts.Core.UI.Binding
{
    public class UIBinder
    {
        private readonly List<IUIUnbind> _bindings = new List<IUIUnbind>();

        public UIBindButtonElement BindButton(Button element)
        {
            var obj = new UIBindButtonElement { Target = element };
            _bindings.Add(obj);
            return obj;
        }

        public UIBindValuableElement<T> Bind<T>(INotifyValueChanged<T> element)
        {
            var obj = new UIBindValuableElement<T> { Target = element };
            _bindings.Add(obj);
            return obj;
        }

        public void UnbindAll()
        {
            foreach (var binding in _bindings)
            {
                binding.Unbind();
            }
        }
    }
}