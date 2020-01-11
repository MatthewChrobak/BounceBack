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
            if (type == VisibleFeatureType.Clothes && this._isMale) {
                return this;
            }
            if (type == VisibleFeatureType.Bottom && this._isFemale) {
                return this;
            }

            var features = VisibleFeature.VisibleFeatures[type];
            this._person.SetFeature(type, features.Random());
            return this;
        }
    }

    public static class Extensions
    {
        public static VisibleFeature Random(this IEnumerable<VisibleFeature> collection) {
            int index = RNG.Next(0, collection.Count());
            return collection.ElementAt(index);
        }
    }
}
