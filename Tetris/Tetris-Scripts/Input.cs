using System;
using System.Threading.Tasks;

namespace Tetris.Scripts
{
    public class InputManager
    {
        private static bool isRunning = false;
        public static event Action<ConsoleKey>? OnInputReceived;

        public static void StartListening()
        {
            if (isRunning) return;
            isRunning = true;

            //input task start
            Task.Run(() =>
            {
                //mag alleen gerunt worden als isRunning true is
                while (isRunning)
                {
                    //lees de key als readkey true is en slaat hem op in var key
                    var key = Console.ReadKey(true).Key;
                    //speelt de event af met key als parameter
                    OnInputReceived?.Invoke(key);
                }
            });
        }

        public static void StopListening()
        {
            isRunning = false;
        }
    }
}
