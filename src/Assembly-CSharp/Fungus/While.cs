using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012B5 RID: 4789
	[CommandInfo("Flow", "While", "Continuously loop through a block of commands while the condition is true. Use the Break command to force the loop to terminate immediately.", 0)]
	[AddComponentMenu("")]
	public class While : If
	{
		// Token: 0x060073E2 RID: 29666 RVA: 0x002AC96C File Offset: 0x002AAB6C
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

		// Token: 0x060073E3 RID: 29667 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
