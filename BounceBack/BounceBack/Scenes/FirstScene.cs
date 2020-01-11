using System.Threading.Tasks;
using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using BounceBack.Models;
using BounceBack.Scenes.Elements;
using BounceBack.Containers;

namespace BounceBack.Scenes
{
    public class FirstScene : Scene
    {
        public CasinoLineQueue _casinoLineQueue;

        private const string _timelimitEvent = "set-time-limit";
        private TopScrollbar topScrollbar;
        private int _timeLimit = 5000;

        private readonly ActionButtonContainer _actionButtonContainer;

        public FirstScene()
        {
            this._casinoLineQueue = new CasinoLineQueue();
            this._casinoLineQueue.AddNewPersonToBack();
            this._casinoLineQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();
            //this._casinoQueue.AddNewPersonToBack();

            this._actionButtonContainer = new ActionButtonContainer()
            {
                AcceptButtonEvent = this.ActionButtonClicked,
                RejectButtonEvent = this.ActionButtonClicked
            };
            this._actionButtonContainer.SetPositionUpdateChildrenRelative(300, 500);

            topScrollbar = new TopScrollbar();

            this.AddChild(_actionButtonContainer);
            this.AddChild(topScrollbar);
            //StartTimeLimit();

            //this.Events.AddEvent("test", PriorityType.LOGIC, () => {
            //    GetTimeRatio();
            //    return ControlEvent.NONE;
            //}, 500, 0);
        }

        private void ActionButtonClicked()
        {
            this._casinoLineQueue.RemovePersonAtFront();
            this._casinoLineQueue.AddNewPersonToBack();
        }

        public override void Draw(ICanvas canvas)
        {
            this._casinoLineQueue.Draw(canvas);
            base.Draw(canvas);
        }

        private ControlEvent TimeLimit()
        {
            //FailureCount++
            //Remove person from queue
            Debug.Log("TimeLimitEvent");
            return ControlEvent.NONE;
        }

        public void StartTimeLimit()
        {
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);
        }

        public void ResetTimeLimit()
        {
            if (this.Events.RemoveEvent(_timelimitEvent))
            {
                Debug.Log("Removed TimeLimitEvent");
            }
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);
        }

        public void GetTimeRatio()
        {
            float ratio;

            GameEvent eventTimer = this.Events.GetEvent(_timelimitEvent);

            if (eventTimer != null)
            {
                ratio = 1 - (eventTimer.GetInterval() / (float)_timeLimit);
                Debug.Log(ratio.ToString());
            }


        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e)
        {
            if (e.Key == KeyboardKey.Tilde)
            {
                Debug.ToggleDebugOverlay();
            }
        }

        //public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        //{
        //    base.HandleMouseButtonPressed(e);

        //    topScrollbar.PressedOnButtons(e.WorldX, e.WorldY);
        //    ResetTimeLimit();
        //}
    }
}