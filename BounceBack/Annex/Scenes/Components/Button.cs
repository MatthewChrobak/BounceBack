using Annex.Graphics.Events;

namespace Annex.Scenes.Components
{
    public class Button : Label
    {
        public delegate void OnClickHandlerDelegate(Button button, MouseButtonEvent e);

        public event OnClickHandlerDelegate OnClickHandler;

        public Button(string elementID = "") : base(elementID) {

        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            base.HandleMouseButtonPressed(e);
            OnClickHandler?.Invoke(this, e);
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            base.HandleMouseButtonReleased(e);
            OnClickHandler?.Invoke(this, e);
        }
    }
}
