using System;

namespace BounceBack
{
    public static class RNG
    {
        private static Random _rng = new Random();

        public static int Next(int lb, int ub) {
            return _rng.Next(lb, ub);
        }
    }
}
