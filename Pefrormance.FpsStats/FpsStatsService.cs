using System.Diagnostics;

namespace Pefrormance.FpsStats
{
    public class FpsStatsService
    {
        private readonly Stopwatch _timeDelta = new Stopwatch();
        private readonly List<double> _fpsDataPoints = new List<double>();
        private Action<Action>? _invokeOnThread;
        private bool _isFpsUpdating;

        public double Fps { get; private set; }
        public double Latency { get; private set; }
        public string Stats { get; private set; } = String.Empty;

        public event EventHandler<EventArgs>? StatsUpdated;

        public FpsStatsService()
        {
        }

        private async Task StartUpdateTimestamp()
        {
            if (_isFpsUpdating)
                return;

            _isFpsUpdating = true;
            _fpsDataPoints.Clear();

            var collectFpsTask = Task.Run(async () =>
            {
                while (_isFpsUpdating)
                {
                    await Task.Delay(100);
                    if (!_timeDelta.IsRunning)
                    {
                        _timeDelta.Restart();
                        _invokeOnThread?.Invoke(CollectFpsStats);
                    }
                }
            });

            var calcFpsTask = Task.Run(async () =>
            {
                while (_isFpsUpdating)
                {
                    await Task.Delay(1000);
                    _invokeOnThread?.Invoke(CalcFpsStats);
                }
            });

            await Task.WhenAll(collectFpsTask, calcFpsTask);
        }

        private void CollectFpsStats()
        {
            var fps = Math.Min(999, 1000d / _timeDelta.ElapsedMilliseconds);
            _fpsDataPoints.Add(fps);
            _timeDelta.Stop();
        }

        private void CalcFpsStats()
        {
            var avgFps = _fpsDataPoints.Count > 0 ? _fpsDataPoints.Average() : 1d;
            var avgLatency = 1d / avgFps;
            Latency = avgLatency;
            Fps = avgFps;
            Stats = $"{DateTime.Now:T} | LAT {avgLatency:0.000} s | FPS {avgFps:000}";
            _fpsDataPoints.Clear();
            StatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public Task Start(Action<Action> invokeOnThread)
        {
            if (invokeOnThread == null)
                throw new ArgumentNullException(nameof(invokeOnThread));

            _invokeOnThread = invokeOnThread;
            return StartUpdateTimestamp();
        }

        public Task Stop()
        {
            _isFpsUpdating = false;
            _invokeOnThread = null;
            return Task.CompletedTask;
        }
    }
}