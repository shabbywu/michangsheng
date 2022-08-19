using System;

namespace Fungus
{
	// Token: 0x02000EB7 RID: 3767
	public static class BlockSignals
	{
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06006A6B RID: 27243 RVA: 0x00292E90 File Offset: 0x00291090
		// (remove) Token: 0x06006A6C RID: 27244 RVA: 0x00292EC4 File Offset: 0x002910C4
		public static event BlockSignals.BlockStartHandler OnBlockStart;

		// Token: 0x06006A6D RID: 27245 RVA: 0x00292EF7 File Offset: 0x002910F7
		public static void DoBlockStart(Block block)
		{
			if (BlockSignals.OnBlockStart != null)
			{
				BlockSignals.OnBlockStart(block);
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06006A6E RID: 27246 RVA: 0x00292F0C File Offset: 0x0029110C
		// (remove) Token: 0x06006A6F RID: 27247 RVA: 0x00292F40 File Offset: 0x00291140
		public static event BlockSignals.BlockEndHandler OnBlockEnd;

		// Token: 0x06006A70 RID: 27248 RVA: 0x00292F73 File Offset: 0x00291173
		public static void DoBlockEnd(Block block)
		{
			if (BlockSignals.OnBlockEnd != null)
			{
				BlockSignals.OnBlockEnd(block);
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06006A71 RID: 27249 RVA: 0x00292F88 File Offset: 0x00291188
		// (remove) Token: 0x06006A72 RID: 27250 RVA: 0x00292FBC File Offset: 0x002911BC
		public static event BlockSignals.CommandExecuteHandler OnCommandExecute;

		// Token: 0x06006A73 RID: 27251 RVA: 0x00292FEF File Offset: 0x002911EF
		public static void DoCommandExecute(Block block, Command command, int commandIndex, int maxCommandIndex)
		{
			if (BlockSignals.OnCommandExecute != null)
			{
				BlockSignals.OnCommandExecute(block, command, commandIndex, maxCommandIndex);
			}
		}

		// Token: 0x020016FD RID: 5885
		// (Invoke) Token: 0x060088A8 RID: 34984
		public delegate void BlockStartHandler(Block block);

		// Token: 0x020016FE RID: 5886
		// (Invoke) Token: 0x060088AC RID: 34988
		public delegate void BlockEndHandler(Block block);

		// Token: 0x020016FF RID: 5887
		// (Invoke) Token: 0x060088B0 RID: 34992
		public delegate void CommandExecuteHandler(Block block, Command command, int commandIndex, int maxCommandIndex);
	}
}
