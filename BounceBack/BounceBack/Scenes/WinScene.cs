using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BounceBack.Scenes
{
    class WinScene : ClosableScene
    {
        private readonly TextureContext _winScreen;
        private readonly TextContext _continueText;

        public WinScene()
        {
            _winScreen = new TextureContext("")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._continueText = new TextContext("Press any key to continue...", "default.ttf")
            {
                RenderPosition = Vector.Create(260, 590),
                FontSize = 36,
                FontColor = RGBA.Purple
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