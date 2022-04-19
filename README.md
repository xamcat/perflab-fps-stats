# Performance FPS Stats

Simple FPS Stats service with an ability to check a UI thread responsiveness

## Usage 

Maui example

- Create and start the FPS Service
  ```MAUI
  _fpsService = new FpsStatsService();
  _fpsService.Start(action => Dispatcher.DispatchAsync(action));
  ```
- Read FPS stats periodically
  ```MAUI
  CounterLabel.Text = _fpsService.Stats;
  ```
- Stop service when not needed
  ```MAUI
  _fpsService.Stop();
  ```
