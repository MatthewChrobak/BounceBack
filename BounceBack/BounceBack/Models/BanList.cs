using System;
using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class BanList
    {
        private HashSet<BanListEntry> BannedPeople;

        public event Action<BanListEntry> OnAddToBanList;

        public BanList() {
            this.BannedPeople = new HashSet<BanListEntry>();
        }

        public void AddPerson(Person person, IEnumerable<VisibleFeatureType> features) {
            this.BannedPeople.Add(new BanListEntry(person, features));
            this.OnAddToBanList?.Invoke(this.BannedPeople.Last());
        }
    }

    public class BanListEntry
    {
        public readonly Person BannedPerson;
        public List<string> DisplayString;

        public BanListEntry(Person person, IEnumerable<VisibleFeatureType> bannedFeatures) {
            this.BannedPerson = person;
            this.DisplayString = new List<string>();

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
                return p.Equals(this.BannedPerson);
            }
            if (obj is BanListEntry b) {
                return b.BannedPerson.Equals(this.BannedPerson);
            }
            return false;
        }

        public override int GetHashCode() {
            return BannedPerson.GetHashCode();
        }
    }
}
