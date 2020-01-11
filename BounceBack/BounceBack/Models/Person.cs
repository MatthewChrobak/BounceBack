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

        public Person() {
            this._features = new VisibleFeature[(int)VisibleFeatureType.VISIBLEFEATURETYPE_COUNT];
            this._skinColor = RGBA.Red;
            this._hairColor = RGBA.Blue;
        }

        public static IEnumerable<VisibleFeatureType> GetFeatureRenderOrder() {
            return _featureOrder;
        }

        internal void SetFeature(VisibleFeatureType type, object p) {
            throw new NotImplementedException();
        }

        public void SetFeature(VisibleFeatureType featureType, string textureName) {
            Debug.Assert((int)featureType < this._features.Length);
            var feature = new VisibleFeature(textureName);

            if (featureType == VisibleFeatureType.Hair) {
                feature.TextureContext.RenderColor = this._hairColor;
            }

            this._features[(int)featureType] = feature;
        }

        public VisibleFeature? GetFeature(VisibleFeatureType feature) {
            Debug.Assert((int)feature < this._features.Length);
            return this._features[(int)feature];
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
