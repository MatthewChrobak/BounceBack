using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using BounceBack.Containers;
using BounceBack.Models;
using BounceBack.Scenes.Elements;

namespace BounceBack.Scenes
{
    public class FirstScene : ClosableScene
    {
        public readonly CasinoQueue _casinoQueue;
        public readonly BanList BanList;
        private TopScrollbar topScrollbar;

        private const string _timelimitEvent = "set-time-limit";
        private int _timeLimit = 5000;
        private int timeLimitCount = 0;

        private TextureContext _bouncer;
        private const string _bouncerDecide = "IMG_0332.png";
        private const string _bouncerAccept = "IMG_0342.png";
        private const string _bouncerReject = "IMG_0343.png";

        private TextureContext _timerBar;
        private TextureContext _timerBarBackground;
        private TextContext _timerText;

        private const int _maxPlayerFailures = 3;
        private int _playerFailures = 0;
        private int _playerScore = 0;

        private TextureContext[] _playerFailureImage;
        private TextureContext[] _playerFailureImageBackground;

        private TextContext _playerTextScore;


        private readonly ActionButtonContainer _actionButtonContainer;

        private const string _musicId = "game background music";

        public FirstScene()
        {
            this.BanList = new BanList();
            this._casinoQueue = new CasinoQueue();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
            this._casinoQueue.AddNewPersonToBack();
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

            _playerFailureImage = new TextureContext[_maxPlayerFailures];
            _playerFailureImageBackground = new TextureContext[_maxPlayerFailures];

            DrawPlayerScore();
            DrawBouncer();
            DrawTimerBar();
            StartTimeLimit();
            DrawPlayerFailures();


            BanList.AddPerson(5);
            BanList.AddPerson(5);
            BanList.AddPerson(5);
            BanList.AddPerson(5);
            BanList.AddPerson(5);

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

            AnimateBouncer(_bouncerAccept);
            ResetTimeLimit();
        }

        private void RejectActionButtonClicked()
        {            
            this._casinoQueue.RemovePersonAtFront();
            this._casinoQueue.AddNewPersonToBack();

            AnimateBouncer(_bouncerReject);
            ResetTimeLimit();
        }

        public override void Draw(ICanvas canvas)
        {
            this._casinoQueue.Draw(canvas);
            canvas.Draw(this._timerBarBackground);
            canvas.Draw(this._timerBar);
            canvas.Draw(this._timerText);
            canvas.Draw(this._playerTextScore);
            canvas.Draw(this._bouncer);

            for(int i = 0; i < _maxPlayerFailures; i++)
            {
                canvas.Draw(this._playerFailureImageBackground[i]);
            }

            for(int i = 0; i < _playerFailures; i++)
            {
                if (_playerFailures >= _maxPlayerFailures) return;
                canvas.Draw(this._playerFailureImage[i]);

            }

            base.Draw(canvas);
        }

        private ControlEvent TimeLimit()
        {
            if(timeLimitCount >= 1)
            {
                IncrementPlayerFailures();
                UpdatePlayerScore(-100);

                this._casinoQueue.RemovePersonAtFront();
                this._casinoQueue.AddNewPersonToBack();
            }
            timeLimitCount++;
            return ControlEvent.NONE;
        }
        
        public void StartTimeLimit()
        {
            this.Events.AddEvent(_timelimitEvent, PriorityType.LOGIC, TimeLimit, interval_ms: _timeLimit);            
        }

        public void ResetTimeLimit()
        {
            this.Events.RemoveEvent(_timelimitEvent);
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

        public void DrawPlayerScore()
        {
            _playerTextScore = new TextContext("Score: " + _playerScore.ToString(), "default.ttf")
            {
                RenderPosition = Vector.Create(25, 200),
                FontSize = 50,
                FontColor = RGBA.White,
                UseUIView = true,
                Alignment = new TextAlignment()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Size = Vector.Create(150, 100)
                }
            };
        }

        public void UpdatePlayerScore(int score)
        {            
            _playerScore = System.Math.Clamp(_playerScore + score, 0, int.MaxValue);
            _playerTextScore.RenderText = "Score: " + _playerScore.ToString();
        }

        public void DrawPlayerFailures()
        {            
            for(int i = 0; i < _maxPlayerFailures; i++)
            {
                _playerFailureImageBackground[i] = new TextureContext("IMG_0348.png")
                {
                    RenderPosition = Vector.Create(75 * i, 100),
                    RenderSize = Vector.Create(100, 100),
                    UseUIView = true
                };

                _playerFailureImage[i] = new TextureContext("IMG_0349.png")
                {
                    RenderPosition = Vector.Create(75 * i, 100),
                    RenderSize = Vector.Create(100, 100),
                    UseUIView = true
                };
            }
        }

        public void IncrementPlayerFailures()
        {
            _playerFailures++;
            if(_playerFailures >= _maxPlayerFailures)
            {
                //Gameover scene
            }
        }

        public void DrawTimerBar()
        {
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
                BorderColor = RGBA.Black,
                UseUIView = true,
                Alignment = new TextAlignment()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Size = Vector.Create(150, 100)
                }
            };
        }

        public void DrawBouncer()
        {
            _bouncer = new TextureContext(_bouncerDecide)
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X-725, ServiceProvider.Canvas.GetResolution().Y - 500),
                RenderSize = Vector.Create(750, 500)
            };
        }

        public void AnimateBouncer(string id)
        {
            _bouncer.SourceTextureName = id;
            this.Events.AddEvent("", PriorityType.ANIMATION, () =>
            {
                _bouncer.SourceTextureName = _bouncerDecide;
                return ControlEvent.REMOVE;
            }, 1000, 1000);
        }
    }
}