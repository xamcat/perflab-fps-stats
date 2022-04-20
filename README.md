# PerfLab FPS Stats

Simple FPS Stats service with an ability to check a UI thread responsiveness

## Usage 

Maui example

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
