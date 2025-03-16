using System;
using System.Threading;

namespace Tetris.FrameWork
{
    public class Game
    {
        private bool exit = false;
        private GameTime? gameTime;

        // Set the target frame rate and frame time (in milliseconds)
        const int iterationSpeed = 60;
        const double targetFrameSpeed = 1000.0 / iterationSpeed; // Time per frame in milliseconds

        private double _accumulatedTime = 0;

        public void Run()
        {
            Initialize();
            gameTime = new GameTime();

            DateTime lastTime = DateTime.Now; // Time of the last frame

            while (!exit)
            {
                // Get the time elapsed since the last frame
                DateTime currentTime = DateTime.Now;
                double deltaTime = (currentTime - lastTime).TotalMilliseconds; // Time difference in ms

                lastTime = currentTime; // Update the last time

                _accumulatedTime += deltaTime;

                // Update game logic while accumulated time exceeds the target frame time
                while (_accumulatedTime >= targetFrameSpeed)
                {
                    gameTime.Start();
                    Update(gameTime); // Call your game update logic

                    _accumulatedTime -= targetFrameSpeed; // Reduce accumulated time by target frame time
                }

                // Optionally, sleep to reduce CPU usage if the frame loop is too fast
                // Only sleep if the accumulated time hasn't been handled yet
                if (_accumulatedTime < targetFrameSpeed)
                {
                    Thread.Sleep(1); // Sleep for 1ms to yield CPU while waiting for next frame
                }
            }
        }

        /// <summary>
        /// Update your game logic here (e.g., moving pieces, checking game state, etc.)
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime) {}

        /// <summary>
        /// // Initialize game state here
        /// </summary>
        public virtual void Initialize() {}
    }
}
    