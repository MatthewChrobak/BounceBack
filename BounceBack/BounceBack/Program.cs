using Annex;
using Annex.Resources;
using Annex.Resources.Pak;
using BounceBack.Scenes;

namespace BounceBack
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider.ResourceManagerRegistry.GetOrCreateResourceManager<PakResourceManager>(ResourceType.Textures);
            AnnexGame.Initialize();
            Debug.PackageResourcesToBinary();

            AnnexGame.Start<FirstScene>();
        }
    }
}