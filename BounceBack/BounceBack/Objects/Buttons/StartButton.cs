using Annex;
using Annex.Events;
using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Objects.Buttons
{
    public class StartButton : Button
    {
        public const string AnimationEventId = "start-button-animation-event";

        private readonly string[] _animationTextures;
        private int _animationIndex;

        public StartButton()
        {
            this._animationIndex = 0;

            this._animationTextures = new[]
            {
                "Buttons/StartButton/Blink.png",
                "Buttons/StartButton/Unblinked.png"
            };

            ImageTextureName.Set(_animationTextures[_animationIndex]);
        }

        public GameEvent GetAnimationEvent()
        {
            return new GameEvent(AnimationEventId, () =>
            {
                this.ImageTextureName.Set(this._animationTextures[++this._animationIndex % this._animationTextures.Length]);
                return ControlEvent.NONE;
            }, 200, 0);
        }
    }
}
