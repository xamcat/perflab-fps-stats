# PerfLab FPS Stats

A simple UI thread performance stats service for your dotnet app which approximately measures UI thread latency and FPS (frames per second) the is handling in real-time.

![Demo](PerfLab.FpsStats.Demo.gif)

## Usage 

Dotnet MAUI example

- Create and start the FPS Service
  ```MAUI
  using PerfLab.FpsStats; 
  ...
  _fpsService = new FpsStatsService();
  _fpsService.Start(action => Dispatcher.DispatchAsync(action));
  ```
- Read FPS stats periodically
  ```MAUI
  CounterLabel.Text = _fpsService.Stats;
  ```
- Subscribe for updates (optional)
  ```
  _fpsService.StatsUpdated += _fpsService_StatsUpdated;

  private void _fpsService_StatsUpdated(object sender, EventArgs e)
  {
      CounterLabel.Text = _fpsService.Stats;
  }
  ```
- Stop service when not needed and unsubscribe from updates 
  ```MAUI
  _fpsService.StatsUpdated -= _fpsService_StatsUpdated;
  _fpsService.Stop();
  ```
