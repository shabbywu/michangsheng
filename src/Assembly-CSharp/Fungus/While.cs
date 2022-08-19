using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E5F RID: 3679
	[CommandInfo("Flow", "While", "Continuously loop through a block of commands while the condition is true. Use the Break command to force the loop to terminate immediately.", 0)]
	[AddComponentMenu("")]
	public class While : If
	{
		// Token: 0x0600674E RID: 26446 RVA: 0x00289F44 File Offset: 0x00288144
		public override void OnEnter()
		{
			bool flag = true;
			if (this.variable != null)
			{
				flag = this.EvaluateCondition();
			}
			End end = null;
			for (int i = this.CommandIndex + 1; i < this.ParentBlock.CommandList.Count; i++)
			{
				End end2 = this.ParentBlock.CommandList[i] as End;
				if (end2 != null && end2.IndentLevel == this.indentLevel)
				{
					end = end2;
					break;
				}
			}
			if (flag)
			{
				end.Loop = true;
				this.Continue();
				return;
			}
			this.Continue(end.CommandIndex + 1);
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
