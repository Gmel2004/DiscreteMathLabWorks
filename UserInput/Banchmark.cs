using System.Diagnostics;

namespace Banchmark
{
    public static class Banchmark
    {
        public static double MeasureTime(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Restart();
            action();
            stopWatch.Stop();
            return stopWatch.Elapsed.TotalSeconds;
        }
    }
}
