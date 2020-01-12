using Annex;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using BounceBack.Objects.Buttons;

namespace BounceBack.Scenes
{
    public class CreditsScene : ClosableScene
    {
        private readonly BackButton _backButton;

        private readonly TextureContext _backgroundTextureContext;

        public CreditsScene()
        {
            this._backButton = new BackButton();
            this._backButton.OnClickHandler += GoBackToMenu;
            this._backButton.Position.Set(780, 500);

            this.AddChild(_backButton);

            this._backgroundTextureContext = new TextureContext("credits background.png")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
        }

        private void GoBackToMenu(Button button, MouseButtonEvent e)
        {
            ServiceProvider.SceneManager.LoadScene<MainMenuScene>();
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._backgroundTextureContext);
            base.Draw(canvas);
        }
    }
}
