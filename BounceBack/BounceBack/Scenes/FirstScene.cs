using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using BounceBack.Models;
using BounceBack.Scenes.Elements;

namespace BounceBack.Scenes
{
    public class FirstScene : Scene
    {
        public CasinoQueue _casionQueue;

        private const string _timelimitEvent = "set-time-limit";
        private TopScrollbar topScrollbar;
        private int _timeLimit = 5000;

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

            topScrollbar = new TopScrollbar();
            //StartTimeLimit();

            this.Events.AddEvent("test", PriorityType.LOGIC, () => {
                GetTimeRatio();
                return ControlEvent.NONE;
            }, 500, 0);
        }

        public override void Draw(ICanvas canvas) {
            this._casionQueue.Draw(canvas);
            base.Draw(canvas);
            topScrollbar.Draw(canvas);
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
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms:_timeLimit);
        }

        public void ResetTimeLimit()
        {
            if(this.Events.RemoveEvent(_timelimitEvent))
            {
                Debug.Log("Removed TimeLimitEvent");
            }
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);
        }

        public void GetTimeRatio()
        {
            float ratio;

            GameEvent eventTimer = this.Events.GetEvent(_timelimitEvent);

            if(eventTimer != null)
            {
                ratio = 1 - ( eventTimer.GetInterval()/ (float)_timeLimit);
                Debug.Log(ratio.ToString());
            }
            

        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            base.HandleMouseButtonPressed(e);

            topScrollbar.PressedOnButtons(e.WorldX, e.WorldY);
            ResetTimeLimit();
        }
    }
}