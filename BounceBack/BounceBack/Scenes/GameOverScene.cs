using Annex;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace BounceBack.Scenes
{
    public class GameOverScene : ClosableScene
    {
        public readonly TextureContext _gameOverTextureContext;

        public GameOverScene()
        {
            this._gameOverTextureContext = new TextureContext("")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._gameOverTextureContext);
            base.Draw(canvas);
        }
    }
}
