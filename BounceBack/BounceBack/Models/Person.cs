using Annex;
using Annex.Data;
using System;
using System.Collections.Generic;

namespace BounceBack.Models
{
    public class Person
    {
        private static VisibleFeatureType[] _featureOrder;

        private VisibleFeature?[] _features;
        private RGBA _skinColor;
        private RGBA _hairColor;

        static Person() {
            _featureOrder = new VisibleFeatureType[] {
                VisibleFeatureType.Arm,
                VisibleFeatureType.NakedBody,
                VisibleFeatureType.Bottom,
                VisibleFeatureType.HeadShapes,
                VisibleFeatureType.Clothes,
                VisibleFeatureType.Mouths,
                VisibleFeatureType.Noses,
                VisibleFeatureType.Ears,
                VisibleFeatureType.EyeSockets,
                VisibleFeatureType.Shoes,
                VisibleFeatureType.Accessories,
            };
        }

        public IEnumerable<VisibleFeatureType> GetRandomFeatures(int numberOfFeatures) {
            var vals = new List<VisibleFeatureType>();
            var results = new List<VisibleFeatureType>();
            foreach (var feature in (VisibleFeatureType[])(Enum.GetValues(typeof(VisibleFeatureType)))) {
                if (feature == VisibleFeatureType.VISIBLEFEATURETYPE_COUNT) {
                    continue;
                }
                vals.Add(feature);
            }

            numberOfFeatures = Math.Max(1, numberOfFeatures);
            numberOfFeatures = Math.Min(5, numberOfFeatures);

            for (int i = 0; i < numberOfFeatures; i++) {
                int index = RNG.Next(0, vals.Count);
                results.Add(vals[index]);
                vals.RemoveAt(index);
            }

            return results;
        }

        public static Person New() {
            var builder = new PersonBuilder()
                .WithFeature(VisibleFeatureType.Accessories)
                .WithFeature(VisibleFeatureType.Bottom)
                .WithFeature(VisibleFeatureType.NakedBody)
                .WithFeature(VisibleFeatureType.Clothes)
                .WithFeature(VisibleFeatureType.EyeSockets)
                .WithFeature(VisibleFeatureType.HeadShapes)
                .WithFeature(VisibleFeatureType.Mouths)
                .WithFeature(VisibleFeatureType.Noses)
                .WithFeature(VisibleFeatureType.Shoes)
                ;
            return builder.Build();
        }

        public Person() {
            this._features = new VisibleFeature[(int)VisibleFeatureType.VISIBLEFEATURETYPE_COUNT];
            this._skinColor = RGBA.Red;
            this._hairColor = RGBA.Blue;
        }

        public static IEnumerable<VisibleFeatureType> GetFeatureRenderOrder() {
            return _featureOrder;
        }

        public void SetFeature(VisibleFeatureType featureType, VisibleFeature visibleFeature)
        {
            Debug.Assert((int)featureType < this._features.Length);
            var feature = visibleFeature;

            if (featureType == VisibleFeatureType.Hair)
            {
                feature.TextureContext.RenderColor = this._hairColor;
            }

            this._features[(int)featureType] = feature;
        }

        public VisibleFeature? GetFeature(VisibleFeatureType feature) {
            Debug.Assert((int)feature < this._features.Length);
            return this._features[(int)feature];
        }

        public bool Equals(Person p, IEnumerable<VisibleFeatureType> features) {
            foreach (var feature in features) {
                var f1 = p.GetFeature(feature)?.TextureContext.SourceTextureName;
                var f2 = this.GetFeature(feature)?.TextureContext.SourceTextureName;
                if (f1 == null || f1 == null) {
                    continue;
                }
                if (f1 != f2) {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object? obj) {
            if (obj is Person p) {
                if (!p._skinColor.Equals(this._skinColor)) {
                    return false;
                }
                if (!p._hairColor.Equals(this._hairColor)) {
                    return false;
                }

                for (int i = 0; i < (int)VisibleFeatureType.VISIBLEFEATURETYPE_COUNT; i++) {
                    if (this._features[i] == null && p._features[i] != null) {
                        return false;
                    }
                    if (this._features[i] != null && p._features[i] == null) {
                        return false;
                    }
                    if (p._features[i]?.Equals(this._features[i]) != true) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode() {
            int hashCode = 0;
            hashCode += this._skinColor?.GetHashCode() ?? 0;
            hashCode += this._hairColor?.GetHashCode() ?? 0;
            return hashCode;
        }
    }
}
