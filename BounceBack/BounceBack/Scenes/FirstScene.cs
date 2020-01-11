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
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
            this._casionQueue.GeneratePerson();
        }

        public override void Draw(ICanvas canvas) {
            this._casionQueue.Draw(canvas);
            base.Draw(canvas);
        }
    }
}
