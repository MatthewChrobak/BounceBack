using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace BounceBack.Scenes.Elements
{
    public class TopScrollbar : UIElement
    {
        private int numOfWantedPeople = 6;

        private TextureContext _leftArrow;
        private TextureContext _rightArrow;

        private TextureContext[] _wantedPeople;

        public TopScrollbar() : base("")
        {
            this._leftArrow = new TextureContext("icon-1.png")
            {
                RenderPosition = Vector.Create(0, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._rightArrow = new TextureContext("icon-1.png")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 100, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._wantedPeople = new TextureContext[numOfWantedPeople];

            for (int i = 0; i < numOfWantedPeople; i++)
            {
                this._wantedPeople[i] = new TextureContext("blank.png")
                {
                    RenderPosition = Vector.Create(100 + 100 * i, 0),
                    RenderSize = Vector.Create(50, 50),
                    UseUIView = true
                };
            }
        }

        public override void Draw(ICanvas canvas)
        {
            for (int i = 0; i < numOfWantedPeople; i++)
            {
                canvas.Draw(this._wantedPeople[i]);
            }
            canvas.Draw(this._leftArrow);
            canvas.Draw(this._rightArrow);
        }

        public void PressedOnButtons(float posX, float posY)
        {
            if (posX <= ServiceProvider.Canvas.GetResolution().X / 5 && posY <= 100)
            {
                HandleLeftButton();
            }
            else if (posX >= ServiceProvider.Canvas.GetResolution().X * 4 / 5 && posY <= 100)
            {
                HandleRightButton();
            }
        }

        public void HandleLeftButton()
        {
            for (int i = 0; i < numOfWantedPeople; i++)
            {
                this._wantedPeople[i].RenderPosition.Set(this._wantedPeople[i].RenderPosition.X - 100, this._wantedPeople[i].RenderPosition.Y);
            }
        }

        public void HandleRightButton()
        {
            for (int i = 0; i < numOfWantedPeople; i++)
            {
                this._wantedPeople[i].RenderPosition.Set(this._wantedPeople[i].RenderPosition.X + 100, this._wantedPeople[i].RenderPosition.Y);
            }
        }
    }
}