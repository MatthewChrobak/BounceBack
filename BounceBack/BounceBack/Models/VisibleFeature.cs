using Annex;
using Annex.Data.Shared;
using Annex.Graphics.Contexts;
using Annex.Resources;
using System.Collections.Generic;

namespace BounceBack.Models
{
    public class VisibleFeature
    {
        public readonly TextureContext TextureContext;

        public VisibleFeature(string textureName) {
            this.TextureContext = new TextureContext(textureName) {
                RenderPosition = Vector.Create(),
                RenderSize = new ScalingVector(Vector.Create(160, 270), Vector.Create())
            };
        }

        internal static IEnumerable<string> GetFeaturesTextures(VisibleFeatureType type) {
            var textures = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Textures);
            return textures.GetResourcesWithPrefix(type.ToString());
        }   
    }
}
