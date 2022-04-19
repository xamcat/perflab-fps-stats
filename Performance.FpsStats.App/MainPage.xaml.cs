using Microsoft.Maui.Dispatching;
using Pefrormance.FpsStats;

namespace Performance.FpsStats.App
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
            _fpsService.Start(action => Dispatcher.DispatchAsync(action));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _fpsService.Stop();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = _fpsService.Stats;

            SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}