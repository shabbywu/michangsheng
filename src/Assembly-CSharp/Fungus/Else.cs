using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001203 RID: 4611
	[CommandInfo("Flow", "Else", "Marks the start of a command block to be executed when the preceding If statement is False.", 0)]
	[AddComponentMenu("")]
	public class Else : Command, INoCommand
	{
		// Token: 0x060070D7 RID: 28887 RVA: 0x002A3514 File Offset: 0x002A1714
		public override void OnEnter()
		{
			if (this.ParentBlock == null)
			{
				return;
			}
			if (this.CommandIndex >= this.ParentBlock.CommandList.Count - 1)
			{
				this.StopParentBlock();
				return;
			}
			int indentLevel = this.indentLevel;
			for (int i = this.CommandIndex + 1; i < this.ParentBlock.CommandList.Count; i++)
			{
				Command command = this.ParentBlock.CommandList[i];
				if (command.IndentLevel == indentLevel && command.GetType() == typeof(End))
				{
					this.Continue(command.CommandIndex + 1);
					return;
				}
			}
			this.StopParentBlock();
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
