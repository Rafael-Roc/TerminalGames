using System;
using Tetris.Scripts.Blocks;

namespace Tetris.Scripts;

public class BlockQueue
{
    private readonly Block[] blocks = new Block[]
    {
        //new rBlock()
        new IBlock(),
        new JBlock(),
        new LBlock(),
        new OBlock(),
        new SBlock(),
        new TBlock(),
        new ZBlock()
    };

    private readonly Random random = new Random();

    public Block NextBlock {get; private set;}

    public BlockQueue()
    {
        NextBlock = RandomBlock();
    }

    private Block RandomBlock()
    {
        return blocks[random.Next(blocks.Length)];
    }

    public Block GetAndUpdate()
    {
        Block block = NextBlock;

        do
        {
            NextBlock = RandomBlock();
        }
        while (block.Id == NextBlock.Id);

        return block;
    }
}
