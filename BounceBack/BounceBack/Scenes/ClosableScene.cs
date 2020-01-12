using Annex;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace BounceBack.Scenes
{
    public class ClosableScene : Scene
    {
        public override void HandleCloseButtonPressed()
        {
            ServiceProvider.SceneManager.LoadScene<GameClosing>();
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            if (e.Key == KeyboardKey.Escape)
            {
                ServiceProvider.SceneManager.LoadScene<GameClosing>();
            }

            base.HandleKeyboardKeyReleased(e);
        }
    }
}
