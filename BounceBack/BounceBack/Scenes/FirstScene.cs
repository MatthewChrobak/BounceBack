using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using BounceBack.Scenes.Elements;

namespace BounceBack.Scenes
{
    public class FirstScene : Scene
    {
        private const string _timelimitEvent = "set-time-limit";

        private TopScrollbar topScrollbar;

        private int _timeLimit = 5000;

        public FirstScene() : base()
        {
            topScrollbar = new TopScrollbar();
            //StartTimeLimit();

            this.Events.AddEvent("test", PriorityType.LOGIC, () => {
                GetTimeRatio();
                return ControlEvent.NONE;
            }, 500, 0);
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

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            topScrollbar.Draw(canvas);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            base.HandleMouseButtonPressed(e);

            topScrollbar.PressedOnButtons(e.WorldX, e.WorldY);
            ResetTimeLimit();
        }
    }
}