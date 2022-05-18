using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001205 RID: 4613
	[CommandInfo("Flow", "End", "Marks the end of a conditional block.", 0)]
	[AddComponentMenu("")]
	public class End : Command
	{
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060070E1 RID: 28897 RVA: 0x0004CA14 File Offset: 0x0004AC14
		// (set) Token: 0x060070E2 RID: 28898 RVA: 0x0004CA1C File Offset: 0x0004AC1C
		public virtual bool Loop { get; set; }

		// Token: 0x060070E3 RID: 28899 RVA: 0x002A35C4 File Offset: 0x002A17C4
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

		// Token: 0x060070E4 RID: 28900 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CloseBlock()
		{
			return true;
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}
	}
}
