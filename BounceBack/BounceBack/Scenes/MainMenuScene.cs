using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using BounceBack.Objects.Buttons;

namespace BounceBack.Scenes
{
    public class MainMenuScene : ClosableScene
    {
        private readonly TextureContext _backgroundTextureContext;
        private readonly StartButton _startButton;
        private const string _musicId = "main menu background music";

        public MainMenuScene()
        {
            this._backgroundTextureContext = new TextureContext("Main Menu Background.png");
            this._startButton = new StartButton();
            this._startButton.OnClickHandler += this.ChangeSceneToGameScene;

            this.AddChild(this._startButton);

            this._backgroundTextureContext.RenderSize = ServiceProvider.Canvas.GetResolution();
            this._startButton.Size.Set(ServiceProvider.Canvas.GetResolution());

            ServiceProvider.AudioManager.PlayBufferedAudio("mainmenu.wav", _musicId, true);

            this.Events.AddEvent(PriorityType.GRAPHICS, this._startButton.GetAnimationEvent());
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._backgroundTextureContext);
            base.Draw(canvas);
        }

        private void ChangeSceneToGameScene(Button button, MouseButtonEvent e)
        {
            if (button == this._startButton && e is MouseButtonReleasedEvent ev && ev.Button == MouseButton.Left)
            {
                ServiceProvider.AudioManager.StopAllAudio(_musicId);
                ServiceProvider.SceneManager.LoadScene<FirstScene>();
            }
        }
    }
}
