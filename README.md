# Performance Fps Stats

Simple FPS Stats service with an ability to check a UI thread responsiveness

## Usage

- Create and start the FPS Service
  ```MAUI
  _fpsService = new FpsStatsService(action => Dispatcher.DispatchAsync(action));
  _fpsService.Start();
  ```
- Read FPS stats periodically
  ```MAUI
  CounterLabel.Text = _fpsService.Stats;
  ```
- Stop service when not needed
  ```MAUI
  _fpsService.Stop();
  ```
