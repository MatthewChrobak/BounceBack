using Annex;
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
    }
}
