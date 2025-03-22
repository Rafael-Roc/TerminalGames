using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Tetris.FrameWork;
using Tetris.Scripts;

namespace Tetris;

public class Tetris : Game
{
    float _elapsedTime;
    GameState gameState = new GameState();

    public override void Initialize()
    {
        Console.CursorVisible = false;
        Console.TreatControlCAsInput = true;

        InputManager.StartListening();
        InputManager.OnInputReceived += Input;


        System.Console.WriteLine($"00:00 \n");
        DrawGrid(gameState.GameGrid);
    }

    public override void Update(GameTime gameTime)
    {
        if (gameState.GameOver)
        {
            Console.Clear();
            System.Console.WriteLine("GAME OVER");
            return;
        }

        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= 0.5f)
        {
            Timer(gameTime);
            gameState.MoveBlockDown();


            _elapsedTime = 0;
        }
        Draw(gameState);
        base.Update(gameTime);
    }

    /// <summary>
    /// Draws the grid on screen based on the information from the grid, 
    /// 0 for empty blocks
    /// 1 for blocks
    /// </summary>
    /// <param name="grid"></param>
    public void DrawGrid(GameGrid grid)
    {
        Console.SetCursorPosition(0, 2);
        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                int id = grid[r, c];
                Console.ForegroundColor = GetBlockColor(id);
                Console.Write(GetBlockCharacter(id));
            }
            System.Console.WriteLine();
        }
    }

    /// <summary>
    /// Draws the moving block on the grid
    /// </summary>
    /// <param name="block"></param>
    public void DrawBlock(Block block)
    {
        foreach (Position p in block.TilePositions())
        {
            Console.SetCursorPosition(p.Column * 3, p.Row + 2);
            Console.ForegroundColor = GetBlockColor(block.Id);
            Console.Write(GetBlockCharacter(block.Id));
        }
    }

    public void InitializeBorders(GameGrid grid)
    {
        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                if (r < grid.BorderSize || r >= grid.Rows - grid.BorderSize || c < grid.BorderSize || c >= grid.Columns - grid.BorderSize)
                {
                    grid[r, c] = 1;
                }
            }
        }
    }

    private void Draw(GameState gameState)
    {
        DrawGrid(gameState.GameGrid);
        DrawBlock(gameState.CurrentBlock);
        InitializeBorders(gameState.GameGrid);
    }

    private void Timer(GameTime pGameTime)
    {
        int totalSeconds = (int)pGameTime.TotalGameTime.TotalSeconds;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        Console.SetCursorPosition(0, 0);
        System.Console.WriteLine($"{minutes:D2}:{seconds:D2} \n");
    }

    private string GetBlockCharacter(int id)
    {
        switch (id)
        {
            case 0:
                return " . ";
            case 1:
                return "[~]";
            default:
                return "[-]";
        }
    }

    private ConsoleColor GetBlockColor(int id)
    {
        ConsoleColor[] blockColor = { ConsoleColor.Gray, ConsoleColor.Gray, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkYellow, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Red };
        return blockColor[id];
    }

    private void Input(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.A:
                gameState.MoveBlockLeft();
                break;
            case ConsoleKey.D:
                gameState.MoveBlockRight();
                break;
            case ConsoleKey.S:
                gameState.MoveBlockDown();
                break;
            case ConsoleKey.E:
                gameState.RotateBlockCW();
                break;
            case ConsoleKey.Q:
                gameState.RotateBlockCCW();
                break;
        }
    }
}
