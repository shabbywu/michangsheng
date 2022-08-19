using System;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x020009CC RID: 2508
	public class AddCommand
	{
		// Token: 0x060045C4 RID: 17860 RVA: 0x001D90FB File Offset: 0x001D72FB
		[YarnCommand("AddWuDaoDian")]
		public static void AddWuDaoDian(int num)
		{
			Tools.instance.getPlayer()._WuDaoDian += num;
		}
	}
}
