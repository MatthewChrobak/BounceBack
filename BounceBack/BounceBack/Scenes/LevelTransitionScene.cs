using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using BounceBack.Models;
using Annex.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BounceBack.Scenes
{
    class LevelTransitionScene : ClosableScene
    {

        private readonly TextureContext _background;
        private readonly TextContext _levelText;

        private readonly int _timeForTransition = 3000;

        public LevelTransitionScene()
        {
            this._background = new TextureContext("level change.png")
            {
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };

            this._levelText = new TextContext("Level " + ScoreSingleton.Instance.GetDifficultyLevel(), "default.ttf")
            {
                RenderPosition = Vector.Create(0, ServiceProvider.Canvas.GetResolution().Y - 100),
                FontSize = 60,
                UseUIView = true
            };

            RenderTextLocation();

            this.Events.AddEvent("load-next-level", PriorityType.LOGIC, LoadNextLevel, 100, _timeForTransition);
        }

        public void RenderTextLocation()
        {
            if(ScoreSingleton.Instance.GetDifficultyLevel() == 2)
            {
                this._levelText.RenderPosition = Vector.Create(0, ServiceProvider.Canvas.GetResolution().Y - 100);
            }
            else
            {
                this._levelText.RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 250, 0);
            }
        }

        public void RenderLevelBackground()
        {
            this._background.SourceTextureName = "";
        }

        public ControlEvent LoadNextLevel()
        {
            ServiceProvider.SceneManager.LoadScene<FirstScene>(true);
            return ControlEvent.REMOVE;
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(this._background);
            canvas.Draw(this._levelText);
            base.Draw(canvas);
        }
    }
}
