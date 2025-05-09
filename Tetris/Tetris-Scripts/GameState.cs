using System;

namespace Tetris.Scripts;

public class GameState
{
    private Block currentBlock;

    public Block CurrentBlock
    {
        get => currentBlock;
        private set
        {
            currentBlock = value;
            currentBlock.Reset();
        }
    }

    public GameGrid GameGrid { get; }
    public BlockQueue BlockQueue { get; }
    public bool GameOver { get; private set; }

    public GameState()
    {
        GameGrid = new GameGrid(22, 12, 1);
        BlockQueue = new BlockQueue();
        CurrentBlock = BlockQueue.GetAndUpdate();
    }

    private bool BlockFits()
    {
        foreach (Position p in CurrentBlock.TilePositions())
        {
            if (!GameGrid.IsEmpty(p.Row, p.Column))
            {
                return false;
            }
        }

        return true;
    }

    public void RotateBlockCW()
    {
        CurrentBlock.RotateCW();

        if (!BlockFits())
        {
            CurrentBlock.RotateCCW();
        }
    }

    public void RotateBlockCCW()
    {
        CurrentBlock.RotateCCW();

        if (!BlockFits())
        {
            CurrentBlock.RotateCW();
        }
    }

    public void MoveBlockLeft()
    {
        CurrentBlock.Move(0, -1);

        if (!BlockFits())
        {
            CurrentBlock.Move(0, 1);
        }
    }

    public void MoveBlockRight()
    {
        CurrentBlock.Move(0, 1);

        if (!BlockFits())
        {
            CurrentBlock.Move(0, -1);
        }
    }

    private bool IsGameOver()
    {
        return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
    }

    private void PlaceBlock()
    {
        //System.Console.WriteLine("BlockPlaced");

        foreach (Position p in CurrentBlock.TilePositions())
        {
            GameGrid[p.Row, p.Column] = CurrentBlock.Id;
        }

        GameGrid.ClearFullRows();

        if (IsGameOver())
        {
            GameOver = true;
        }
        else
        {
            CurrentBlock = BlockQueue.GetAndUpdate();
        }
    }

    public void MoveBlockDown()
    {
        CurrentBlock.Move(1, 0);
        
        if (!BlockFits())
        {
            CurrentBlock.Move(-1, 0);
            PlaceBlock();
        }
    }


}
