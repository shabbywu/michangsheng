using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F80 RID: 3968
	[CommandInfo("YSTools", "开关传音符功能", "开关传音符功能", 0)]
	[AddComponentMenu("")]
	public class CtrCy : Command
	{
		// Token: 0x06006F36 RID: 28470 RVA: 0x002A697A File Offset: 0x002A4B7A
		public override void OnEnter()
		{
			Tools.instance.getPlayer().emailDateMag.IsStopAll = this.IsStop.Value;
			this.Continue();
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005BF7 RID: 23543
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable IsStop;
	}
}
