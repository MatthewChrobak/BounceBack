using System;
using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Resources;
using System.Linq;
using System.Threading;

namespace BounceBack.Scenes
{
    public class LoadingScene : ClosableScene
    {
        private readonly SolidRectangleContext _loadingTextureContext;
        private readonly SolidRectangleContext _loadingBackgroundTextureContext;
        private readonly SolidRectangleContext _loadingBorderTextureContext;
        private readonly TextureContext _backgroundTextureContext;
        private readonly TextureContext _blurBackgroundTextureContext;

        private float LoadPercentage => (float)_loadAmount / _totalLoadAmount;
        private int _loadAmount;
        private int _totalLoadAmount;

        public LoadingScene()
        {
            this._loadAmount = 0;
            this._totalLoadAmount = 0;

            var screenResolution = ServiceProvider.Canvas.GetResolution();
            var renderSize = Vector.Create(500, 30);
            
            var x = (screenResolution.X - renderSize.X) / 2f;
            var y = (screenResolution.Y - renderSize.Y) / 2f + 100;

            this._loadingTextureContext = new SolidRectangleContext(RGBA.White)
            {
                RenderSize = Vector.Create(renderSize.X, renderSize.Y),
                RenderPosition = Vector.Create(x, y)
            };

            this._loadingBackgroundTextureContext = new SolidRectangleContext(RGBA.Black)
            {
                RenderSize = Vector.Create(renderSize.X, renderSize.Y),
                RenderPosition = Vector.Create(x, y)
            };

            this._loadingBorderTextureContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderSize = Vector.Create(renderSize.X, renderSize.Y),
                RenderPosition = Vector.Create(x, y),
                RenderBorderColor = RGBA.Purple,
                RenderBorderSize = 5
            };

            this._backgroundTextureContext = new TextureContext("Main Menu Background.png")
            {
                RenderSize = screenResolution
            };

            this._blurBackgroundTextureContext = new TextureContext("Blur.png")
            {
                RenderSize = screenResolution,
                RenderColor = RGBA.White
            };

            Load();
        }

        private void Load()
        {
            void LoadAll()
            {
                LoadAllTextures();

                ServiceProvider.SceneManager.LoadScene<MainMenuScene>();
            }

            new Thread(LoadAll).Start();
        }

        private void LoadAllTextures()
        {
            var resourceManager = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Textures);
            var allTextures = resourceManager?.GetResourcesWithPrefix("");

            if (allTextures != null)
            {
                this._totalLoadAmount = allTextures.Count();

                foreach (var texture in allTextures)
                {
                    if (texture == _backgroundTextureContext.SourceTextureName.Value?.ToLowerInvariant() ||
                        texture == _blurBackgroundTextureContext.SourceTextureName.Value?.ToLowerInvariant())
                    {
                        continue;
                    }
                    resourceManager?.Load(texture);
                    this._loadAmount++;
                }
            }
        }

        private void UpdateLoadingBar()
        {
            this._loadingTextureContext.RenderSize.Set(this.LoadPercentage * 500, 30);
            
            var renderColor = this._blurBackgroundTextureContext.RenderColor;
            if (renderColor != null)
            {
                renderColor.A = (byte) (255f - (255f * this.LoadPercentage * 0.6));
            }
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._backgroundTextureContext);
            canvas.Draw(this._blurBackgroundTextureContext);
            base.Draw(canvas);
            canvas.Draw(this._loadingBorderTextureContext);
            canvas.Draw(this._loadingBackgroundTextureContext);
            UpdateLoadingBar();
            canvas.Draw(_loadingTextureContext);
        }
    }
}
