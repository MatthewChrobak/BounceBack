using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class CasinoQueue : IDrawableObject
    {
        private readonly TextureContext _background;
        private readonly SolidRectangleContext _fog;
        private readonly List<Person> _peopleInLine;
        private readonly PersonRenderer[] _renderers;
        public Person? PersonInFront => this._peopleInLine.FirstOrDefault();

        private class PersonRenderer
        {
            public readonly Vector RenderSizeScale;
            public readonly Vector PositionOffset;

            public PersonRenderer(Vector size, Vector position) {
                this.RenderSizeScale = size;
                this.PositionOffset = position;
            }
        }

        public void RemovePersonAtFront() {
            this._peopleInLine.RemoveAt(0);
        }

        public CasinoQueue() {
            this._peopleInLine = new List<Person>();
            this._renderers = new PersonRenderer[] {
                new PersonRenderer(Vector.Create(2, 2), Vector.Create(300, 125)),
                new PersonRenderer(Vector.Create(1.75f, 1.75f), Vector.Create(250, 150)),
                new PersonRenderer(Vector.Create(1.5f, 1.5f), Vector.Create(200, 200)),
                new PersonRenderer(Vector.Create(1.25f, 1.25f), Vector.Create(150, 225)),
                new PersonRenderer(Vector.Create(1.1f, 1.1f), Vector.Create(100, 250)),

                new PersonRenderer(Vector.Create(0.9f, 0.9f), Vector.Create(75, 270)),
                new PersonRenderer(Vector.Create(0.8f, 0.8f), Vector.Create(40, 280)),
                new PersonRenderer(Vector.Create(0.8f, 0.8f), Vector.Create(10, 280)),
                new PersonRenderer(Vector.Create(0.8f, 0.8f), Vector.Create(-20, 280)),
            };
            this._background = new TextureContext("backgroundgame.png") {
                RenderPosition = Vector.Create(),
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
            this._fog = new SolidRectangleContext(new Annex.Data.RGBA(0, 0, 0, 15)) {
                RenderPosition = Vector.Create(),
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
        }

        public void Draw(ICanvas canvas) {
            canvas.Draw(this._background);
            // We need to draw from back to front, but not too far back.
            for (int i = this._renderers.Length - 1; i >= 0; i--) {
                canvas.Draw(this._fog);

                if (i < this._peopleInLine.Count) {
                    var person = this._peopleInLine[i];
                    var renderer = this._renderers[i];

                    foreach (var feature in Person.GetFeatureRenderOrder()) {
                        var ctx = person.GetFeature(feature)?.TextureContext;

                        if (ctx == null) {
                            continue;
                        }

                        if (feature == VisibleFeatureType.Hair) {
                            ctx.RenderColor = person._hairColor;
                        }
                        if (feature == VisibleFeatureType.Arms ||
                            feature == VisibleFeatureType.Ears ||
                            feature == VisibleFeatureType.EyeSockets ||
                            feature == VisibleFeatureType.HeadShapes ||
                            feature == VisibleFeatureType.Mouths ||
                            feature == VisibleFeatureType.NakedBody ||
                            feature == VisibleFeatureType.Noses
                        ) {
                            ctx.RenderColor = person._skinColor;
                                    }

                        ctx.RenderPosition.Set(renderer.PositionOffset);
                        ctx.RenderSize.Set(renderer.RenderSizeScale);
                        canvas.Draw(ctx);
                    }
                }
            }
        }

        public void AddNewPersonToBack() {
            this._peopleInLine.Add(Person.New_PossiblyBanned());
        }
    }
}
