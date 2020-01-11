using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class StartButton : Button
    {
        public const string AnimationEventId = "start-button-animation-event";

        private readonly TextureContext _blinkerTextureContext;
        private readonly string[] _animationTextures;
        private int _animationIndex;
        private bool _renderBlinkers;

        public StartButton()
        {
            this._animationIndex = 0;
            this._renderBlinkers = true;

            this._animationTextures = new[]
            {
                "Buttons/StartButton/1.png",
                "Buttons/StartButton/2.png",
                "Buttons/StartButton/3.png",
                "Buttons/StartButton/4.png",
                "Buttons/StartButton/5.png",
                "Buttons/StartButton/6.png",
                "Buttons/StartButton/7.png",
                "Buttons/StartButton/8.png",
                "Buttons/StartButton/9.png",
                "Buttons/StartButton/10.png",
                "Buttons/StartButton/11.png",
                "Buttons/StartButton/12.png",
                "Buttons/StartButton/13.png",
                "Buttons/StartButton/14.png"
            };

            _blinkerTextureContext = new TextureContext(this._animationTextures[this._animationIndex])
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            ImageTextureName.Set("Buttons/StartButton/Start Button.png");
        }

        public GameEvent GetAnimationEvent()
        {
            return new GameEvent(AnimationEventId, () =>
            {
                this._blinkerTextureContext.SourceTextureName.Set(this._animationTextures[++this._animationIndex % this._animationTextures.Length]);
                return ControlEvent.NONE;
            }, 200, 0);
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            if (this._renderBlinkers)
            {
                canvas.Draw(this._blinkerTextureContext);
            }
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            this._renderBlinkers = false;
            base.HandleMouseButtonPressed(e);
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            this._renderBlinkers = true;
            base.HandleMouseButtonReleased(e);
        }
    }
}
