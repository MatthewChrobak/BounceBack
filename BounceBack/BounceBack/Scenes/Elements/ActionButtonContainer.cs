using System;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using BounceBack.Objects.Buttons;

namespace BounceBack.Scenes.Elements
{
    public class ActionButtonContainer : Container
    {
        public Action AcceptButtonEvent { get; set; }
        public Action RejectButtonEvent { get; set; }

        private readonly AcceptButton _acceptButton;
        private readonly RejectButton _rejectButton;

        public ActionButtonContainer()
        {
            _acceptButton = new AcceptButton();
            _acceptButton.OnClickHandler += ButtonClicked;

            _rejectButton = new RejectButton();
            _rejectButton.OnClickHandler += ButtonClicked;
        }

        private void ButtonClicked(Button button, MouseButtonEvent e)
        {
            if (e is MouseButtonPressedEvent && e.Button == MouseButton.Left)
            {
                if (button == _acceptButton)
                {
                    AcceptButtonEvent?.Invoke();
                }

                if (button == _rejectButton)
                {
                    RejectButtonEvent?.Invoke();
                }
            }
        }

        public void SetPositionUpdateChildrenRelative(float x, float y)
        {
            Position.Set(x, y);

            _acceptButton.Position.Set(new OffsetVector(Position, 150, 0));
            _rejectButton.Position.Set(new OffsetVector(Position, 0, 0));
        }

        public override void Draw(ICanvas canvas)
        {
            AddChild(_acceptButton);
            AddChild(_rejectButton);

            base.Draw(canvas);
        }
    }
}
