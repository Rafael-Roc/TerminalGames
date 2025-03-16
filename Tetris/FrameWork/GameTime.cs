using System;

namespace Tetris.FrameWork
{
    public class GameTime
    {
        // TimeSpan to track the total time elapsed since the game started
        public TimeSpan TotalGameTime { get; private set; }
        // TimeSpan to track the time elapsed between the last frame
        public TimeSpan ElapsedGameTime { get; private set; }

        private DateTime _lastFrameTime; // DateTime to track the last frame time

        public GameTime()
        {
            TotalGameTime = TimeSpan.Zero;
            ElapsedGameTime = TimeSpan.Zero;
            _lastFrameTime = DateTime.Now;  // Initialize the last frame time
        }

        // Start method calculates the time elapsed from the last frame
        public void Start()
        {
            DateTime currentFrameTime = DateTime.Now;  // Get the current time

            // Calculate the time elapsed since the last frame
            ElapsedGameTime = currentFrameTime - _lastFrameTime;

            // Update the total game time
            TotalGameTime += ElapsedGameTime;

            // Store the current frame's time for the next frame
            _lastFrameTime = currentFrameTime;
        }
    }
}