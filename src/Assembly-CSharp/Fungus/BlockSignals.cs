using System;

namespace Fungus
{
	// Token: 0x02001340 RID: 4928
	public static class BlockSignals
	{
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060077A8 RID: 30632 RVA: 0x002B526C File Offset: 0x002B346C
		// (remove) Token: 0x060077A9 RID: 30633 RVA: 0x002B52A0 File Offset: 0x002B34A0
		public static event BlockSignals.BlockStartHandler OnBlockStart;

		// Token: 0x060077AA RID: 30634 RVA: 0x0005191F File Offset: 0x0004FB1F
		public static void DoBlockStart(Block block)
		{
			if (BlockSignals.OnBlockStart != null)
			{
				BlockSignals.OnBlockStart(block);
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060077AB RID: 30635 RVA: 0x002B52D4 File Offset: 0x002B34D4
		// (remove) Token: 0x060077AC RID: 30636 RVA: 0x002B5308 File Offset: 0x002B3508
		public static event BlockSignals.BlockEndHandler OnBlockEnd;

		// Token: 0x060077AD RID: 30637 RVA: 0x00051933 File Offset: 0x0004FB33
		public static void DoBlockEnd(Block block)
		{
			if (BlockSignals.OnBlockEnd != null)
			{
				BlockSignals.OnBlockEnd(block);
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060077AE RID: 30638 RVA: 0x002B533C File Offset: 0x002B353C
		// (remove) Token: 0x060077AF RID: 30639 RVA: 0x002B5370 File Offset: 0x002B3570
		public static event BlockSignals.CommandExecuteHandler OnCommandExecute;

		// Token: 0x060077B0 RID: 30640 RVA: 0x00051947 File Offset: 0x0004FB47
		public static void DoCommandExecute(Block block, Command command, int commandIndex, int maxCommandIndex)
		{
			if (BlockSignals.OnCommandExecute != null)
			{
				BlockSignals.OnCommandExecute(block, command, commandIndex, maxCommandIndex);
			}
		}

		// Token: 0x02001341 RID: 4929
		// (Invoke) Token: 0x060077B2 RID: 30642
		public delegate void BlockStartHandler(Block block);

		// Token: 0x02001342 RID: 4930
		// (Invoke) Token: 0x060077B6 RID: 30646
		public delegate void BlockEndHandler(Block block);

		// Token: 0x02001343 RID: 4931
		// (Invoke) Token: 0x060077BA RID: 30650
		public delegate void CommandExecuteHandler(Block block, Command command, int commandIndex, int maxCommandIndex);
	}
}
