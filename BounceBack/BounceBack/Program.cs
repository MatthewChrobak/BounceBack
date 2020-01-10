using Annex;
using BounceBack.Scenes;

namespace BounceBack
{
    public class Program
    {
        private static void Main(string[] args) {
            AnnexGame.Initialize();
            AnnexGame.Start<FirstScene>();
        }
    }
}
