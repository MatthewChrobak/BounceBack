using System;
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

            switch (RNG.Next(0, 5)) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                default:
                    break;
            }

            switch (RNG.Next(0, 2)) {
                case 0:
                    
                case 1:
                case 2:
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
