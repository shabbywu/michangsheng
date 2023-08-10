namespace Fungus;

public static class BlockSignals
{
	public delegate void BlockStartHandler(Block block);

	public delegate void BlockEndHandler(Block block);

	public delegate void CommandExecuteHandler(Block block, Command command, int commandIndex, int maxCommandIndex);

	public static event BlockStartHandler OnBlockStart;

	public static event BlockEndHandler OnBlockEnd;

	public static event CommandExecuteHandler OnCommandExecute;

	public static void DoBlockStart(Block block)
	{
		if (BlockSignals.OnBlockStart != null)
		{
			BlockSignals.OnBlockStart(block);
		}
	}

	public static void DoBlockEnd(Block block)
	{
		if (BlockSignals.OnBlockEnd != null)
		{
			BlockSignals.OnBlockEnd(block);
		}
	}

	public static void DoCommandExecute(Block block, Command command, int commandIndex, int maxCommandIndex)
	{
		if (BlockSignals.OnCommandExecute != null)
		{
			BlockSignals.OnCommandExecute(block, command, commandIndex, maxCommandIndex);
		}
	}
}
