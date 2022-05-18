using System;
using Yarn.Unity;

namespace script.YarnEditor.Command
{
	// Token: 0x02000AB5 RID: 2741
	public class AddCommand
	{
		// Token: 0x06004623 RID: 17955 RVA: 0x000322C0 File Offset: 0x000304C0
		[YarnCommand("AddWuDaoDian")]
		public static void AddWuDaoDian(int num)
		{
			Tools.instance.getPlayer()._WuDaoDian += num;
		}
	}
}
