using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class AcceptButton : Button
    {
        private const string NormalButtonTexture = "Buttons/GoButton/Go unpressed.png";
        private const string PressedButtonTexture = "Buttons/GoButton/Go pressed.png";

        public AcceptButton()
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
