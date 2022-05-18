using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001437 RID: 5175
	[CommandInfo("YSTools", "开关传音符功能", "开关传音符功能", 0)]
	[AddComponentMenu("")]
	public class CtrCy : Command
	{
		// Token: 0x06007D26 RID: 32038 RVA: 0x00054A8C File Offset: 0x00052C8C
		public override void OnEnter()
		{
			Tools.instance.getPlayer().emailDateMag.IsStopAll = this.IsStop.Value;
			this.Continue();
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ACB RID: 27339
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStop;
	}
}
