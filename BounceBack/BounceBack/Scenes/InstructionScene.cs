using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;

namespace BounceBack.Scenes
{
    public class InstructionScene : ClosableScene
    {
        private readonly TextureContext _instructionTextureContext;
        private readonly TextContext _continueTextContext;

        public InstructionScene()
        {
            this._instructionTextureContext = new TextureContext("instructions.png")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._continueTextContext = new TextContext("Press any key to continue...", "default.ttf")
            {
                RenderPosition = Vector.Create(260, 590),
                FontSize = 36,
                FontColor = RGBA.Purple
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._instructionTextureContext);
            base.Draw(canvas);
            canvas.Draw(this._continueTextContext);
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            ServiceProvider.SceneManager.LoadScene<FirstScene>(true);
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            ServiceProvider.SceneManager.LoadScene<FirstScene>(true);
        }
    }
}
