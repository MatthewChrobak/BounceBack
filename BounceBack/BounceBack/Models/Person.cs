using Annex;
using Annex.Data;
using BounceBack.Scenes;
using System;
using System.Collections.Generic;

namespace BounceBack.Models
{
    public class Person
    {
        private static VisibleFeatureType[] _featureOrder;

        private VisibleFeature?[] _features;
        public RGBA _skinColor;
        public string SkinColorName;
        public RGBA _hairColor;
        public string HairColorName;

        static Person() {
            _featureOrder = new VisibleFeatureType[] {
                VisibleFeatureType.Arms,
                VisibleFeatureType.NakedBody,
                VisibleFeatureType.Bottom,
                VisibleFeatureType.HeadShapes,
                VisibleFeatureType.Clothes,
                VisibleFeatureType.Mouths,
                VisibleFeatureType.Noses,
                VisibleFeatureType.Ears,
                VisibleFeatureType.EyeSockets,
                VisibleFeatureType.Hair,
                VisibleFeatureType.Shoes,
                VisibleFeatureType.Arms,
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
                if (feature == VisibleFeatureType.NakedBody) {
                    continue;
                }
                if (feature == VisibleFeatureType.Arms) {
                    continue;
                }
                if (feature == VisibleFeatureType.Ears) {
                    continue;
                }
                if (feature == VisibleFeatureType.Moles) {
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
                .WithFeature(VisibleFeatureType.Clothes)
                .WithFeature(VisibleFeatureType.Accessories)
                .WithFeature(VisibleFeatureType.Bottom)
                .WithFeature(VisibleFeatureType.NakedBody)
                .WithFeature(VisibleFeatureType.EyeSockets)
                .WithFeature(VisibleFeatureType.Arms)
                .WithFeature(VisibleFeatureType.HeadShapes)
                .WithFeature(VisibleFeatureType.Mouths)
                .WithFeature(VisibleFeatureType.Noses)
                .WithFeature(VisibleFeatureType.Hair)
                .WithFeature(VisibleFeatureType.Shoes)
                ;
            return builder.Build();
        }

        public static Person New_PossiblyBanned() {
            if (RNG.Next(0, 100) < 10) {
                var scene = ServiceProvider.SceneManager.CurrentScene as FirstScene;
                if (scene != null) {
                    var person = scene.BanList.GetRandomPerson();
                    if (person != null) {
                        return person;
                    }
                }
            }
            return Person.New();
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
