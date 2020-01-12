using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class RejectButton : Button
    {
        private const string NormalButtonTexture = "newbackandgobuttons/img_3760.png";
        private const string PressedButtonTexture = "newbackandgobuttons/img_3759.png";

        public RejectButton()
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
