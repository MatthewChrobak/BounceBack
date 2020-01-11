using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class PersonBuilder
    {
        private Person _person;

        public PersonBuilder() {
            this._person = new Person();
        }

        public Person Build() {
            return this._person;
        }

        public PersonBuilder MakeNew() {
            this._person = new Person();
            return this;
        }

        public PersonBuilder WithFeature(VisibleFeatureType type) {
            var features = VisibleFeature.GetFeaturesTextures(type);
            this._person.SetFeature(type, features.Random());
            return this;
        }
    }

    public static class Extensions
    {
        public static string Random(this IEnumerable<string> collection) {
            int index = RNG.Next(0, collection.Count());
            return collection.ElementAt(index);
        }
    }
}
