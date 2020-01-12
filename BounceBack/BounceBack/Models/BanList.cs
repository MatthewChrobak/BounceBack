using Annex.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class BanList
    {
        private HashSet<BanListEntry> BannedPeople;

        public event Action<BanListEntry>? OnAddToBanList;

        public BanList() {
            this.BannedPeople = new HashSet<BanListEntry>();
        }

        public void AddPerson(int numberOfFeatures) {
            int oldCount = this.BannedPeople.Count;
            while (BannedPeople.Count == oldCount) {
                var person = Person.New();
                var entry = new BanListEntry(person, person.GetRandomFeatures(numberOfFeatures));
                if (entry.BannedFeatures.Count() == 0) {
                    continue;
                }
                this.BannedPeople.Add(entry);
            }
            this.OnAddToBanList?.Invoke(this.BannedPeople.Last());
        }

        public GameEvent GenerateEvents() {
            return new GameEvent("", () => {
                AddPerson(5);
                return ControlEvent.NONE;
            }, 5000, 5000);
        }

        public bool ContainsPerson(Person person) {
            return this.BannedPeople.Any(entry => entry.Equals(person));
        }

        public Person? GetRandomPerson() {
            if (BannedPeople.Count == 0) {
                return null;
            }
            int index = RNG.Next(0, BannedPeople.Count);
            return BannedPeople.ElementAt(index).BannedPerson;
        }
    }

    public class BanListEntry
    {
        public readonly Person BannedPerson;
        public List<string> DisplayString;
        public IEnumerable<VisibleFeatureType> BannedFeatures;

        public BanListEntry(Person person, IEnumerable<VisibleFeatureType> bannedFeatures) {
            this.BannedPerson = person;
            this.DisplayString = new List<string>();
            this.BannedFeatures = bannedFeatures;

            foreach (var featureType in bannedFeatures) {
                var feature = BannedPerson.GetFeature(featureType);
                if (feature == null) {
                    continue;
                }
                string texture = feature.TextureContext.SourceTextureName.Value;
                texture = texture.Remove(0, texture.IndexOf('/') + 1);
                this.DisplayString.Add($"{featureType}: {texture.Substring(0, texture.IndexOf('.'))}");
            }
        }

        public override bool Equals(object? obj) {
            if (obj is Person p) {
                return p.Equals(this.BannedPerson, this.BannedFeatures);
            }
            if (obj is BanListEntry b) {
                return b.BannedPerson.Equals(this.BannedPerson, this.BannedFeatures);
            }
            return false;
        }

        public override int GetHashCode() {
            return BannedPerson.GetHashCode();
        }
    }
}
