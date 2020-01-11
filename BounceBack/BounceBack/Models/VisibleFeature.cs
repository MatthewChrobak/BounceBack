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
        public static IDictionary<VisibleFeatureType, List<VisibleFeature>> VisibleFeatures = Load();

        public static Dictionary<VisibleFeatureType, List<VisibleFeature>> Load()
        {
            var dictionary = new Dictionary<VisibleFeatureType, List<VisibleFeature>>();

            var resourceManager = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Textures);

            foreach (var visibleFeatureType in (VisibleFeatureType[]) Enum.GetValues(typeof(VisibleFeatureType)))
            {
                if (visibleFeatureType == VisibleFeatureType.VISIBLEFEATURETYPE_COUNT)
                {
                    continue;
                }

                var textures = resourceManager?.GetResourcesWithPrefix(visibleFeatureType.ToString());

                if (textures != null)
                    foreach (var texture in textures)
                    {
                        if (!dictionary.ContainsKey(visibleFeatureType))
                        {
                            dictionary[visibleFeatureType] = new List<VisibleFeature> {new VisibleFeature(texture)};
                        }
                        else
                        {
                            dictionary[visibleFeatureType].Add(new VisibleFeature(texture));
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
