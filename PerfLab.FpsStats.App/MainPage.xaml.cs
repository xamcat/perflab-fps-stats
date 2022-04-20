using Microsoft.Maui.Dispatching;
using PerfLab.FpsStats;

namespace PerfLab.FpsStats.App
{
    public partial class MainPage : ContentPage
    {
        private readonly FpsStatsService _fpsService = new FpsStatsService();
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _fpsService.StatsUpdated += _fpsService_StatsUpdated;
            _fpsService.Start(action => Dispatcher.DispatchAsync(action));
        }

        private void _fpsService_StatsUpdated(object sender, EventArgs e)
        {
            CounterLabel.Text = _fpsService.Stats;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _fpsService.StatsUpdated -= _fpsService_StatsUpdated;
            _fpsService.Stop();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = _fpsService.Stats;

            SemanticScreenReader.Announce(CounterLabel.Text);
        }

        private void OnHeavyUITaskClicked(object sender, EventArgs e)
        {
            // TODO: test your UI Heavy logic here

            var result = 0d;
            for (int i = 0; i < 10000000; i++)
            {
                result += Math.Sin(i) + Math.Cos(i) + Math.Acos(i);
            }

            Console.WriteLine(result);
        }
    }
}