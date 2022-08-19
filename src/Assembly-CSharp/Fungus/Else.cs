using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC9 RID: 3529
	[CommandInfo("Flow", "Else", "Marks the start of a command block to be executed when the preceding If statement is False.", 0)]
	[AddComponentMenu("")]
	public class Else : Command, INoCommand
	{
		// Token: 0x06006458 RID: 25688 RVA: 0x0027E534 File Offset: 0x0027C734
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

		// Token: 0x06006459 RID: 25689 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
