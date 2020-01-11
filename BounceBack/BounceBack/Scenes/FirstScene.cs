using Annex.Events;
using Annex.Graphics;
using Annex.Scenes.Components;
using BounceBack.Models;

namespace BounceBack.Scenes
{
    public class FirstScene : Scene
    {
        public CasinoQueue _casionQueue;

        public FirstScene() {
            this._casionQueue = new CasinoQueue();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();
            this._casionQueue.AddNewPersonToBack();

            this.Events.AddEvent("", PriorityType.LOGIC, () => {
                this._casionQueue.RemovePersonAtFront();
                this._casionQueue.AddNewPersonToBack();
                return ControlEvent.NONE;
            }, 1000);
        }

        public override void Draw(ICanvas canvas) {
            this._casionQueue.Draw(canvas);
            base.Draw(canvas);
        }
    }
}
