using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class AcceptButton : Button
    {
        private const string NormalButtonTexture = "newbackandgobuttons/img_3761.png";
        private const string PressedButtonTexture = "newbackandgobuttons/img_3757.png";

        public AcceptButton()
        {
            ImageTextureName.Set(NormalButtonTexture);
            Size.Set(200, 200);
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
