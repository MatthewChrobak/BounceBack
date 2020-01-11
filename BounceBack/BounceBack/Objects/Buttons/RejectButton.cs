using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class RejectButton : Button
    {
        private const string NormalButtonTexture = "Buttons/BackButton/Back unpressed.png";
        private const string PressedButtonTexture = "Buttons/BackButton/Back pressed.png";

        public RejectButton()
        {
            ImageTextureName.Set(NormalButtonTexture);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            this.ImageTextureName.Set(PressedButtonTexture);
            base.HandleMouseButtonPressed(e);
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            this.ImageTextureName.Set(NormalButtonTexture);
            base.HandleMouseButtonReleased(e);
        }
    }
}
