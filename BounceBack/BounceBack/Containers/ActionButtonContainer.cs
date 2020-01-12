using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using BounceBack.Objects.Buttons;
using BounceBack.Scenes;
using System;

namespace BounceBack.Containers
{
    public class ActionButtonContainer : Container
    {
        public Action AcceptButtonEvent { get; set; }
        public Action RejectButtonEvent { get; set; }

        public readonly AcceptButton _acceptButton;
        public readonly RejectButton _rejectButton;

        public ActionButtonContainer() {
            _acceptButton = new AcceptButton();
            _acceptButton.OnClickHandler += ButtonClicked;

            _rejectButton = new RejectButton();
            _rejectButton.OnClickHandler += ButtonClicked;
        }

        private void ButtonClicked(Button button, MouseButtonEvent e) {
            if (e is MouseButtonReleasedEvent && e.Button == MouseButton.Left) {
                var scene = ServiceProvider.SceneManager.CurrentScene as FirstScene;
                if (scene == null) {
                    return;
                }

                if (button == _acceptButton) {
                    if (scene.BanList.ContainsPerson(scene._casinoQueue.PersonInFront)) {                                                
                        scene.IncrementPlayerFailures();
                    }
                    
                    AcceptButtonEvent?.Invoke();
                }

                if (button == _rejectButton) {
                    if (!scene.BanList.ContainsPerson(scene._casinoQueue.PersonInFront)) {
                        scene.IncrementPlayerFailures();
                    }
                    
                    RejectButtonEvent?.Invoke();
                }
            }
        }

        public void SetPositionUpdateChildrenRelative(float x, float y) {
            Position.Set(x, y);

            _acceptButton.Position.Set(new OffsetVector(Position, 150, 0));
            _rejectButton.Position.Set(new OffsetVector(Position, 0, 0));
        }

        public override void Draw(ICanvas canvas) {
            AddChild(_acceptButton);
            AddChild(_rejectButton);

            base.Draw(canvas);
        }
    }
}
