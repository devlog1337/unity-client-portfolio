using System.Collections.Generic;
using UnityEngine;

namespace PortfolioSamples.PopupStack
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class PopupLayer : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetInputEnabled(bool enabled)
        {
            _canvasGroup.interactable = enabled;
            _canvasGroup.blocksRaycasts = enabled;
        }
    }

    public sealed class PopupStackController
    {
        private readonly Stack<PopupLayer> _stack = new();

        public PopupLayer Current => _stack.Count == 0 ? null : _stack.Peek();

        public void Push(PopupLayer popup)
        {
            if (popup == null)
                return;

            Current?.SetInputEnabled(false);
            _stack.Push(popup);
            popup.SetInputEnabled(true);
        }

        public PopupLayer Pop()
        {
            if (_stack.Count == 0)
                return null;

            var closed = _stack.Pop();
            closed.SetInputEnabled(false);
            Current?.SetInputEnabled(true);
            return closed;
        }

        public void Clear()
        {
            while (_stack.Count > 0)
                _stack.Pop().SetInputEnabled(false);
        }
    }
}
