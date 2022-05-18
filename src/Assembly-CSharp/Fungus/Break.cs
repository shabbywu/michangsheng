using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011ED RID: 4589
	[CommandInfo("Flow", "Break", "Force a loop to terminate immediately.", 0)]
	[AddComponentMenu("")]
	public class Break : Command
	{
		// Token: 0x06007068 RID: 28776 RVA: 0x002A2368 File Offset: 0x002A0568
		public override void OnEnter()
		{
			int num = -1;
			int num2 = -1;
			for (int i = this.CommandIndex - 1; i >= 0; i--)
			{
				While @while = this.ParentBlock.CommandList[i] as While;
				if (@while != null)
				{
					num = i;
					num2 = @while.IndentLevel;
					break;
				}
			}
			if (num == -1)
			{
				this.Continue();
				return;
			}
			int j = num + 1;
			while (j < this.ParentBlock.CommandList.Count)
			{
				End end = this.ParentBlock.CommandList[j] as End;
				if (end != null && end.IndentLevel == num2)
				{
					if (this.CommandIndex > num && this.CommandIndex < end.CommandIndex)
					{
						this.Continue(end.CommandIndex + 1);
						return;
					}
					break;
				}
				else
				{
					j++;
				}
			}
			this.Continue();
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
