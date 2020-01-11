using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using BounceBack.Models;
using System.Collections.Generic;

namespace BounceBack.Scenes.Elements
{
    public class TopScrollbar : UIElement
    {
        private TextureContext _leftArrow;
        private TextureContext _rightArrow;
        private Vector _offset;
        private const int width = 200;

        private List<TextContext> _bannedPeopleDescriptions;

        public TopScrollbar() : base("") {
            this._offset = Vector.Create(100, 0);
            this.Size.Set(ServiceProvider.Canvas.GetResolution());
            this._leftArrow = new TextureContext("icon-1.png") {
                RenderPosition = Vector.Create(0, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._rightArrow = new TextureContext("icon-1.png") {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 100, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._bannedPeopleDescriptions = new List<TextContext>();
        }

        internal void OnAddToBanList(BanListEntry bannedPerson) {
            this._bannedPeopleDescriptions.Add(new TextContext(string.Join("\r\n", bannedPerson.DisplayString), "default.ttf") {
                RenderPosition = new OffsetVector(Vector.Create(this._bannedPeopleDescriptions.Count * width, 0), this._offset),
                Alignment = new TextAlignment() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Size = Vector.Create(width, 150),
                    VerticalAlignment = VerticalAlignment.Top
                },
                FontColor = RGBA.White,
                UseUIView = true
            });
        }

        public override void Draw(ICanvas canvas) {
            foreach (var person in this._bannedPeopleDescriptions) {
                canvas.Draw(person);
            }
            canvas.Draw(this._leftArrow);
            canvas.Draw(this._rightArrow);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            var posX = e.MouseX;
            var posY = e.MouseY;
            if (posX <= ServiceProvider.Canvas.GetResolution().X / 5 && posY <= 100) {
                this._offset.Add(-width, 0);
            } else if (posX >= ServiceProvider.Canvas.GetResolution().X * 4 / 5 && posY <= 100) {
                this._offset.Add(width, 0);
            }
        }
    }
}