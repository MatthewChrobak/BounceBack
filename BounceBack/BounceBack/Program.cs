using Annex;
using Annex.Resources.Pak;
using BounceBack.Models;
using BounceBack.Scenes;

namespace BounceBack
{
    public class Program
    {
        private static void Main(string[] args) {
            var x = default(Person);
            ServiceProvider.ResourceManagerRegistry.GetOrCreateResourceManager<PakResourceManager>(Annex.Resources.ResourceType.Textures);

            AnnexGame.Initialize();
            Debug.PackageResourcesToBinary();
            AnnexGame.Start<FirstScene>();
        }
    }
}
