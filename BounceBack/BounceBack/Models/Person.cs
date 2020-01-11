using Annex;
using Annex.Data;
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
                VisibleFeatureType.Bottom,
                VisibleFeatureType.Ears
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
    }
}
