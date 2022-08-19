using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DCB RID: 3531
	[CommandInfo("Flow", "End", "Marks the end of a conditional block.", 0)]
	[AddComponentMenu("")]
	public class End : Command
	{
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06006462 RID: 25698 RVA: 0x0027E5E9 File Offset: 0x0027C7E9
		// (set) Token: 0x06006463 RID: 25699 RVA: 0x0027E5F1 File Offset: 0x0027C7F1
		public virtual bool Loop { get; set; }

		// Token: 0x06006464 RID: 25700 RVA: 0x0027E5FC File Offset: 0x0027C7FC
		public override void OnEnter()
		{
			if (this.Loop)
			{
				for (int i = this.CommandIndex - 1; i >= 0; i--)
				{
					Command command = this.ParentBlock.CommandList[i];
					if (command.IndentLevel == this.IndentLevel && command.GetType() == typeof(While))
					{
						this.Continue(i);
						return;
					}
				}
			}
			this.Continue();
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
