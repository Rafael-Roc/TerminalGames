using System;

namespace Tetris.Scripts;

public class GameGrid
{
    private readonly int[,] grid;

    public int Rows { get; }
    public int Columns { get; }

    public int BorderSize { get; }

    public int this[int r, int c]
    {
        get => grid[r, c];
        set => grid[r, c] = value;
    }

    public GameGrid(int rows, int columns, int bordersize)
    {
        Rows = rows;
        Columns = columns;
        BorderSize = bordersize;
        grid = new int[rows, columns];
    }

    public bool IsInside(int r, int c)
    {
        return r > 0 && r < Rows - BorderSize && c > 0 && c < Columns - BorderSize;
    }

    public bool IsEmpty(int r, int c)
    {
        return IsInside(r, c) && (grid[r, c] == 0 || grid[r, c] == 1);
    }

    public bool IsRowFull(int r)
    {
        for (int c = BorderSize; c < Columns - BorderSize; c++)
        {
            if (grid[r, c] == 0 || grid[r, c] == 1)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsRowEmpty(int r)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (grid[r, c] != 0 && grid[r, c] != 1)
            {
                return false;
            }
        }
        return true;
    }

    private void ClearRow(int r)
    {
        for (int c = 0; c < Columns; c++)
        {
            grid[r, c] = 0;
        }
    }

    private void MoveRowDown(int r, int numRows)
    {
        for (int c = 0; c < Columns; c++)
        {
            if (grid[r, c] != 1)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }
    }

    public int ClearFullRows()
    {
        int cleared = 0;

        for (int r = Rows - 1; r >= 0; r--)
        {
            if (IsRowFull(r))
            {
                ClearRow(r);
                cleared++;
            }
            else if (cleared > 0)
            {
                MoveRowDown(r, cleared);
            }
        }

        return cleared;
    }
}