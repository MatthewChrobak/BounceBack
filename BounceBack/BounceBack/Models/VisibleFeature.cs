using System;
using Annex;
using Annex.Data.Shared;
using Annex.Graphics.Contexts;
using Annex.Resources;
using System.Collections.Generic;
using System.Linq;

namespace BounceBack.Models
{
    public class VisibleFeature
    {
        public static IDictionary<VisibleFeatureType, IEnumerable<VisibleFeature>> VisibleFeatures = Load();

        public static Dictionary<VisibleFeatureType, IEnumerable<VisibleFeature>> Load()
        {
            var dictionary = new Dictionary<VisibleFeatureType, IEnumerable<VisibleFeature>>();

            var resourceManager = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Textures);

            foreach (var visibleFeatureType in (VisibleFeatureType[]) Enum.GetValues(typeof(VisibleFeatureType)))
            {
                var textures = resourceManager?.GetResourcesWithPrefix(visibleFeatureType.ToString());

                foreach (var texture in textures)
                {
                    if (!dictionary.ContainsKey(visibleFeatureType))
                    {
                        dictionary[visibleFeatureType] = new List<VisibleFeature> { new VisibleFeature(texture) };
                    }
                    else
                    {
                        dictionary[visibleFeatureType].Append(new VisibleFeature(texture));
                    }
                }
                
            }

            return dictionary;
        }

        public readonly TextureContext TextureContext;

        public VisibleFeature(string textureName) {
            this.TextureContext = new TextureContext(textureName) {
                RenderPosition = Vector.Create(),
                RenderSize =  new ScalingVector(Vector.Create(160, 270), Vector.Create())
            };
        }

        public override bool Equals(object? obj) {
            if (obj is VisibleFeature feature) {
                return feature.TextureContext.SourceTextureName.Value == this.TextureContext.SourceTextureName.Value;
            }
            return false;
        }

        public override int GetHashCode() {
            return this.TextureContext.SourceTextureName.Value.GetHashCode();
        }
    }
}
