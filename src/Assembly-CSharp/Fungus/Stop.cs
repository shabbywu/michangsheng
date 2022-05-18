using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200129E RID: 4766
	[CommandInfo("Flow", "Stop", "Stop executing the Block that contains this command.", 0)]
	[AddComponentMenu("")]
	public class Stop : Command
	{
		// Token: 0x06007383 RID: 29571 RVA: 0x0004ED07 File Offset: 0x0004CF07
		public override void OnEnter()
		{
			this.StopParentBlock();
		}

		// Token: 0x06007384 RID: 29572 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}
	}
}
