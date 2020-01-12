using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;

namespace BounceBack.Scenes
{
    public class GameOverScene : ClosableScene
    {
        private readonly TextureContext _firedTextureContext;
        private readonly TextureContext _firedHoverNoneTextureContext;
        private readonly TextureContext _firedHoverBossTextureContext;
        private readonly TextureContext _firedHoverBouncerTextureContext;

        private float _blurScalor;
        private Vector? _mousePos;

        public GameOverScene()
        {
            this._blurScalor = 0;

            this._firedTextureContext = new TextureContext("Fired_graphic.PNG")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._firedHoverNoneTextureContext = new TextureContext("Hover_none.PNG")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution(),
                RenderColor = new RGBA(255, 255, 255, 0)
            };

            this._firedHoverBossTextureContext = new TextureContext("Hover_boss.PNG")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._firedHoverBouncerTextureContext = new TextureContext("Hover_bounce.PNG")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this.Events.AddEvent(PriorityType.GRAPHICS, FadeInHoverNoneTextureContext());
            this.Events.AddEvent(PriorityType.INPUT, UpdateMouseMove());
        }

        private GameEvent UpdateMouseMove()
        {
            return new GameEvent("mouse pos",
                () =>
                {
                    var mPos = ServiceProvider.Canvas.GetRealMousePos();
                    _mousePos = Vector.Create(mPos.X, mPos.Y);
                    return ControlEvent.NONE;
                }, 100, 3000);
        }

        private GameEvent FadeInHoverNoneTextureContext()
        {
            return new GameEvent("blur game over background",
                () =>
                {
                    _blurScalor += 1f / (3000/50f);
                    var renderColor = this._firedHoverNoneTextureContext.RenderColor;
                    if (renderColor != null)
                    {
                        var alpha = 255 * _blurScalor;
                        if (alpha < 255)
                        {
                            renderColor.A = (byte)alpha;
                            return ControlEvent.NONE;
                        }

                        renderColor.A = 255;
                        return ControlEvent.REMOVE;
                    }

                    return ControlEvent.NONE;
                }, 50, 1000);
        }

        private bool IsHoverOverBoss()
        {
            return _mousePos != null &&
                   _mousePos.X >= 139 && _mousePos.X <= 309 &&
                   _mousePos.Y >= 291 && _mousePos.Y <= 480;
        }

        private bool IsHoverOverBouncer()
        {
            return _mousePos != null &&
                   _mousePos.X >= 599 && _mousePos.X <= 870 &&
                   _mousePos.Y >= 116 && _mousePos.Y <= 529;
        }

        public override void Draw(ICanvas canvas)
        {
            if (_blurScalor < 1)
            {
                canvas.Draw(this._firedTextureContext);
            }
            
            canvas.Draw(this._firedHoverNoneTextureContext);
            
            base.Draw(canvas);

            if (IsHoverOverBoss())
            {
                canvas.Draw(_firedHoverBossTextureContext);
            }

            if (IsHoverOverBouncer())
            {
                canvas.Draw(_firedHoverBouncerTextureContext);
            }
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (IsHoverOverBoss())
                {
                    ServiceProvider.SceneManager.LoadScene<GameClosing>();
                }
                else if (IsHoverOverBouncer())
                {
                    ServiceProvider.SceneManager.LoadScene<FirstScene>(true);
                }
            }
        }
    }
}
