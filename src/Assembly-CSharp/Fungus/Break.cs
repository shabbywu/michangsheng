using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DB8 RID: 3512
	[CommandInfo("Flow", "Break", "Force a loop to terminate immediately.", 0)]
	[AddComponentMenu("")]
	public class Break : Command
	{
		// Token: 0x060063FD RID: 25597 RVA: 0x0027D0E0 File Offset: 0x0027B2E0
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

		// Token: 0x060063FE RID: 25598 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
