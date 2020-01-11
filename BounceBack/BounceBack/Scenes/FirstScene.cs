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
        private int timeLimitCount = 0;

        public FirstScene() : base()
        {
            topScrollbar = new TopScrollbar();
        }

        private ControlEvent TimeLimit()
        {            
            if(timeLimitCount >= 1)
            {
                //FailureCount++
                //Remove person from queue
                Debug.Log("TimeLimitEvent");
            }
            timeLimitCount++;
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
            timeLimitCount = 0;
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);
        }

        public void StopTimeLimit()
        {
            this.Events.RemoveEvent(_timelimitEvent);
        }

        public float GetTimeRatio()
        {
            float ratio = 0;
            GameEvent eventTimer = this.Events.GetEvent(_timelimitEvent);

            if(eventTimer != null)
            {
                ratio = ( eventTimer.GetTimeBeforeNextInvocation()/ (float)_timeLimit);
            }

            return ratio;
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