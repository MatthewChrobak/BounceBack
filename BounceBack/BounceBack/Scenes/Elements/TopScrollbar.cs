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
        private readonly TextureContext _leftArrow;
        private readonly TextureContext _rightArrow;
        private Vector _offset;
        private const int width = 175;
        private const int height = 110;

        private List<TextContext> _bannedPeopleDescriptions;
        private List<SolidRectangleContext> _descriptionBackgrounds;

        public TopScrollbar() : base("") {
            this._offset = Vector.Create(100, 0);
            this.Size.Set(ServiceProvider.Canvas.GetResolution().X, height);
            this._leftArrow = new TextureContext("topbar/img_3750.png") {
                RenderPosition = Vector.Create(0, 0),
                RenderSize = Vector.Create(100, height),
                UseUIView = true
            };

            this._rightArrow = new TextureContext("topbar/img_3749.png") {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 100, 0),
                RenderSize = Vector.Create(100, height),
                UseUIView = true
            };

            this._bannedPeopleDescriptions = new List<TextContext>();
            this._descriptionBackgrounds = new List<SolidRectangleContext>();
        }

        internal void OnAddToBanList(BanListEntry bannedPerson) {
            this._descriptionBackgrounds.Add(new SolidRectangleContext(new RGBA(255, 246, 159)) {
                RenderPosition = new OffsetVector(Vector.Create(this._descriptionBackgrounds.Count * width + 10, 10), this._offset),
                RenderSize = Vector.Create(width - 20, height - 20),
                UseUIView = true,
                RenderBorderSize = 5,
                RenderBorderColor = new RGBA(255, 237, 77)
            });
            this._bannedPeopleDescriptions.Add(new TextContext(string.Join("\r\n", bannedPerson.DisplayString), "default.ttf") {
                RenderPosition = new OffsetVector(Vector.Create(this._bannedPeopleDescriptions.Count * width + 10, 10), this._offset),
                Alignment = new TextAlignment() {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Size = Vector.Create(width - 20, height - 20),
                    VerticalAlignment = VerticalAlignment.Top
                },
                FontColor = RGBA.Black,
                UseUIView = true
            });
        }

        public override void Draw(ICanvas canvas) {
            foreach (var background in this._descriptionBackgrounds) {
                canvas.Draw(background);
            }
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