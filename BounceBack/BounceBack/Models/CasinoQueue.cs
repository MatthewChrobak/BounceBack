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

            public PersonRenderer(Vector size, Vector position)
            {
                this.RenderSizeScale = size;
                this.PositionOffset = position;
            }
        }

        public void RemovePersonAtFront()
        {
            this._peopleInLine.RemoveAt(0);
        }

        public CasinoQueue()
        {
            this._peopleInLine = new List<Person>();
            this._renderers = new PersonRenderer[] {
                new PersonRenderer(Vector.Create(2, 2), Vector.Create(300, 125)),
                new PersonRenderer(Vector.Create(1.75f, 1.75f), Vector.Create(250, 120)),
                new PersonRenderer(Vector.Create(1.5f, 1.5f), Vector.Create(200, 115)),
                new PersonRenderer(Vector.Create(1.25f, 1.25f), Vector.Create(150, 110)),
                new PersonRenderer(Vector.Create(1.1f, 1.1f), Vector.Create(100, 105)),

                new PersonRenderer(Vector.Create(0.9f, 0.9f), Vector.Create(50, 110)),
                new PersonRenderer(Vector.Create(0.75f, 0.75f), Vector.Create(75, 120)),
                new PersonRenderer(Vector.Create(0.55f, 0.55f), Vector.Create(100, 130)),
                new PersonRenderer(Vector.Create(0.3f, 0.3f), Vector.Create(125, 140)),
            };
            this._background = new TextureContext("background.png")
            {
                RenderPosition = Vector.Create(),
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
            this._fog = new SolidRectangleContext(new Annex.Data.RGBA(0, 0, 0, 30))
            {
                RenderPosition = Vector.Create(),
                RenderSize = ServiceProvider.Canvas.GetResolution()
            };
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._background);
            // We need to draw from back to front, but not too far back.
            for (int i = this._renderers.Length; i >= 0; i--)
            {
                canvas.Draw(this._fog);

                if (i < this._peopleInLine.Count)
                {
                    var person = this._peopleInLine[i];
                    var renderer = this._renderers[i];

                    foreach (var feature in Person.GetFeatureRenderOrder())
                    {
                        var ctx = person.GetFeature(feature)?.TextureContext;

                        if (ctx == null)
                        {
                            continue;
                        }

                        ctx.RenderPosition.Set(renderer.PositionOffset);
                        ctx.RenderSize.Set(renderer.RenderSizeScale);
                        canvas.Draw(ctx);
                    }
                }
            }
        }

        public void AddNewPersonToBack()
        {
            var builder = new PersonBuilder()
                .WithFeature(VisibleFeatureType.Bottom)
                .WithFeature(VisibleFeatureType.Clothes)
                .WithFeature(VisibleFeatureType.EyeSockets)
                .WithFeature(VisibleFeatureType.HeadShapes)
                .WithFeature(VisibleFeatureType.Mouths)
                .WithFeature(VisibleFeatureType.Noses);

            this._peopleInLine.Add(builder.Build());
        }
    }
}
