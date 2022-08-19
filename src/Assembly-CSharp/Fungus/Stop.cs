using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E4D RID: 3661
	[CommandInfo("Flow", "Stop", "Stop executing the Block that contains this command.", 0)]
	[AddComponentMenu("")]
	public class Stop : Command
	{
		// Token: 0x060066F5 RID: 26357 RVA: 0x0028863F File Offset: 0x0028683F
		public override void OnEnter()
		{
			this.StopParentBlock();
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}
	}
}
