using System.IO;
using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;

namespace BounceBack.Scenes
{
    public class InstructionScene : ClosableScene
    {
        private readonly TextureContext _instructionTextureContext;
        private readonly TextContext _continueTextContext;

        private float _fadeInScalor;
        private bool _cutSceneDone;

        public InstructionScene()
        {
            this._cutSceneDone = false;

            this._instructionTextureContext = new TextureContext("instructions.png")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._continueTextContext = new TextContext("Press any key to continue...", "default.ttf")
            {
                RenderPosition = Vector.Create(380, 570),
                FontSize = 36,
                FontColor = new RGBA(120, 120, 120, 0),
                BorderColor = new RGBA(90, 40, 74, 0),
                BorderThickness = 2
            };

            this.Events.AddEvent(PriorityType.GRAPHICS, FadeInContinueTextContext());
        }

        private GameEvent FadeInContinueTextContext()
        {
            return new GameEvent("fade in continue text",
                () =>
                {
                    _cutSceneDone = true;
                    _fadeInScalor += 1f / (3000 / 50f);
                    var fontColor = this._continueTextContext.FontColor;
                    var borderColor = this._continueTextContext.BorderColor;
                    if (fontColor != null && borderColor != null)
                    {
                        var alpha = 255 * _fadeInScalor;
                        if (alpha < 255)
                        {
                            fontColor.A = (byte)alpha;
                            borderColor.A = (byte)(alpha * 0.9);
                            return ControlEvent.NONE;
                        }

                        fontColor.A = 255;
                        borderColor.A = 255;
                        return ControlEvent.REMOVE;
                    }

                    return ControlEvent.NONE;
                }, 50, 3000);
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._instructionTextureContext);
            base.Draw(canvas);
            canvas.Draw(this._continueTextContext);
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            base.HandleKeyboardKeyReleased(e);

            if (!e.Handled && _cutSceneDone)
            {
                ServiceProvider.SceneManager.LoadScene<FirstScene>();
            }
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            if (_cutSceneDone)
            {
                ServiceProvider.SceneManager.LoadScene<FirstScene>();
            }
        }
    }
}
