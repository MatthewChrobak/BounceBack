using Annex.Data;
using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class PersonBuilder
    {
        private Person _person;
        private bool _isMale;
        private bool _isFemale => !this._isMale;

        public PersonBuilder() {
            this._person = new Person();
            this._isMale = RNG.Next(0, 100) < 50;
            byte alpha = 255;
            switch (RNG.Next(0, 8)) {
                case 0:
                    this._person.HairColorName = "red";
                    this._person._hairColor = new RGBA(255, 0, 0, alpha);
                    break;
                case 1:
                    this._person.HairColorName = "blue";
                    this._person._hairColor = new RGBA(0, 0, 255, alpha);
                    break;
                case 2:
                    this._person.HairColorName = "black";
                    this._person._hairColor = new RGBA(0, 0, 0, alpha);
                    break;
                case 3:
                    this._person.HairColorName = "white";
                    this._person._hairColor = new RGBA(255, 255, 255, alpha);
                    break;
                case 4:
                    this._person.HairColorName = "brown";
                    this._person._hairColor = new RGBA(150, 75, 0, alpha);
                    break;
                case 5:
                    this._person.HairColorName = "green";
                    this._person._hairColor = new RGBA(0, 255, 0, alpha);
                    break;
                case 6:
                    this._person.HairColorName = "purple";
                    this._person._hairColor = new RGBA(255, 0, 255, alpha);
                    break;
                default:
                    this._person.HairColorName = "blonde";
                    this._person._hairColor = new RGBA(255, 255, 0, alpha);
                    break;
            }

            switch (RNG.Next(0, 4)) {
                case 0:
                    this._person._skinColor = new RGBA(141, 85, 36, alpha);
                    break;
                case 1:
                    this._person._skinColor = new RGBA(224, 172, 105, alpha);
                    break;
                case 3:
                    this._person._skinColor = new RGBA(247, 225, 211, alpha);
                    break;
                default:
                    this._person._skinColor = new RGBA(255, 219, 172, alpha);
                    break;
            }
        }

        public Person Build() {
            return this._person;
        }

        public PersonBuilder MakeNew() {
            this._person = new Person();
            this._isMale = RNG.Next(0, 100) < 50;
            return this;
        }

        public PersonBuilder WithFeature(VisibleFeatureType type) {
            if (type == VisibleFeatureType.Bottom && this._isFemale) {
                return this;
            }

            var features = VisibleFeature.VisibleFeatures[type];
            var feature = features.RandomOrDefault();

            if (type == VisibleFeatureType.Clothes && this._isMale) {
                while (feature.TextureContext.SourceTextureName.Value.Contains("dress")) {
                    feature = features.RandomOrDefault();
                }
            }

            if (feature != null) {
                this._person.SetFeature(type, feature);
            }
            return this;
        }
    }

    public static class Extensions
    {
        public static VisibleFeature Random(this IEnumerable<VisibleFeature> collection) {
            int index = RNG.Next(0, collection.Count());
            return collection.ElementAt(index);
        }

        public static VisibleFeature? RandomOrDefault(this IEnumerable<VisibleFeature> collection) {
            int count = collection.Count();
            int index = RNG.Next(0, count);
            if (index >= count) {
                return null;
            }
            return collection.ElementAt(index);
        }
    }
}
