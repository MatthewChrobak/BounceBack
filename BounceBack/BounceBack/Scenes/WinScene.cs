using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;

namespace BounceBack.Scenes
{
    class WinScene : ClosableScene
    {
        private readonly TextureContext _winScreen;
        private readonly TextContext _continueText;

        public WinScene()
        {
            _winScreen = new TextureContext("Lvl4.png")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._continueText = new TextContext("Press any key to continue...", "default.ttf")
            {
                RenderPosition = Vector.Create(380, 570),
                FontSize = 36,
                FontColor = new RGBA(120, 120, 120),
                BorderColor = new RGBA(90, 40, 74),
                BorderThickness = 2
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_winScreen);
            base.Draw(canvas);
            canvas.Draw(_continueText);
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            ServiceProvider.SceneManager.LoadScene<MainMenuScene>();
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            ServiceProvider.SceneManager.LoadScene<MainMenuScene>();
        }
    }
}