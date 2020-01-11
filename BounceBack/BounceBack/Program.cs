using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Resources;
using Annex.Resources.FS;
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

            ServiceProvider.AudioManager.PlayAudio("club.wav", loop: true);

            var tracker = new EventTracker(1000);
            ServiceProvider.EventManager.GetEvent(ICanvas.DrawGameEventID).AttachTracker(tracker);
            Debug.AddDebugInformation(() => $"FPS: {tracker.LastCount}");

            AnnexGame.Start<FirstScene>();
        }
    }
}