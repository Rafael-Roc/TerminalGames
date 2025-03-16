using System;
using Tetris;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Tetris.Tetris game = new Tetris.Tetris();
            game.Run();
        }
    }
}