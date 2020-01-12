using Annex;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using BounceBack.Containers;
using BounceBack.Models;
using BounceBack.Scenes.Elements;
using BounceBack.Containers;
using Annex.Graphics.Contexts;
using Annex.Data.Shared;
using Annex.Data;

namespace BounceBack.Scenes
{
    public class FirstScene : ClosableScene
    {
        public readonly CasinoQueue _casinoQueue;
        public readonly BanList BanList;

        private const string _timelimitEvent = "set-time-limit";
        private TopScrollbar topScrollbar;
        private int _timeLimit = 5000;
        private int timeLimitCount = 0;

        private TextureContext _bouncer;
        private TextureContext _timerBar;
        private TextureContext _timerBarBackground;
        private TextContext _timerText;

        private readonly ActionButtonContainer _actionButtonContainer;

        private const string _musicId = "game background music";

        public FirstScene()
        {
            this.BanList = new BanList();
            this._casinoQueue = new CasinoQueue();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();

            this._actionButtonContainer = new ActionButtonContainer()
            {
                AcceptButtonEvent = this.AcceptActionButtonClicked,
                RejectButtonEvent = this.RejectActionButtonClicked
            };
            this._actionButtonContainer.SetPositionUpdateChildrenRelative(300, 500);

            topScrollbar = new TopScrollbar();
            BanList.OnAddToBanList += topScrollbar.OnAddToBanList;

            this.AddChild(_actionButtonContainer);
            this.AddChild(topScrollbar);

            //StartTimeLimit();

            //this.Events.AddEvent("test", PriorityType.LOGIC, () => {
            //    GetTimeRatio();
            //    return ControlEvent.NONE;
            //}, 500, 0);

            DrawBouncer();
            StartTimeLimit();

            BanList.AddPerson(1);

            this.Events.AddEvent("timer", PriorityType.ANIMATION, () =>
            {
                if(timeLimitCount >= 1)
                {
                    if (GetTimeRatio() <= 0.2f)
                    {
                        _timerBar.RenderColor = RGBA.Red;
                    }
                    else if (GetTimeRatio() <= 0.5f)
                    {
                        _timerBar.RenderColor = RGBA.Yellow;
                    }
                    else
                    {
                        _timerBar.RenderColor = RGBA.Green;
                    }
                    _timerBar.RenderSize = new ScalingVector(Vector.Create(150, 25), Vector.Create(GetTimeRatio(), 1));
                    _timerText.RenderText = System.String.Format("{0:0}", System.Math.Ceiling(GetTimeRatio() * 5));
                }
                return ControlEvent.NONE;
            }, 100);

            ServiceProvider.AudioManager.PlayBufferedAudio("club.wav", _musicId, true);
        }

        private void AcceptActionButtonClicked()
        {
            this._casinoQueue.RemovePersonAtFront();
            this._casinoQueue.AddNewPersonToBack();            

            ResetTimeLimit();
        }

        private void RejectActionButtonClicked()
        {
            this._casinoQueue.RemovePersonAtFront();
            this._casinoQueue.AddNewPersonToBack();

            _bouncer.SourceTextureName = "IMG_0331.png";
            this.Events.AddEvent("", PriorityType.ANIMATION, () =>
            {
                _bouncer.SourceTextureName = "IMG_0332.png";
                return ControlEvent.REMOVE;
            }, 1000, 1000);

            ResetTimeLimit();
        }

        public override void Draw(ICanvas canvas)
        {
            this._casinoQueue.Draw(canvas);
            canvas.Draw(this._timerBarBackground);
            canvas.Draw(this._timerBar);
            canvas.Draw(this._timerText);
            canvas.Draw(this._bouncer);
            base.Draw(canvas);
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
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);
            _timerBar = new TextureContext("background.png")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 250, 150),
                RenderSize = new ScalingVector(Vector.Create(150, 25), Vector.Create(GetTimeRatio(), 1)),
                UseUIView = true
            };
            _timerBarBackground = new TextureContext("background.png")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 250, 150),
                RenderSize = Vector.Create(150, 25),
                RenderColor = RGBA.Black,
                UseUIView = true
            };
            _timerText = new TextContext("5.0", "default.ttf")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 250, 100),
                FontSize = 30,
                FontColor = RGBA.White,
                UseUIView = true,
                Alignment = new TextAlignment()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Size = Vector.Create(150,100)
                }
            };
        }

        public void ResetTimeLimit()
        {
            if (this.Events.RemoveEvent(_timelimitEvent))
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

        public Float GetTimeRatio()
        {
            Float ratio = new Float();
            GameEvent eventTimer = this.Events.GetEvent(_timelimitEvent);

            if (eventTimer != null)
            {
                ratio = new Float((float)eventTimer.GetTimeBeforeNextInvocation()/_timeLimit);
            }
            return ratio;
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

        public void DrawBouncer()
        {
            _bouncer = new TextureContext("IMG_0332.png")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X-700, ServiceProvider.Canvas.GetResolution().Y - 500),
                RenderSize = Vector.Create(750, 500)
            };
        }
    }
}