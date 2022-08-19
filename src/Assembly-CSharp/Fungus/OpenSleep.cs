using System;
using script.Sleep;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F9A RID: 3994
	[CommandInfo("YSTools", "打开休息界面", "打开休息界面", 0)]
	[AddComponentMenu("")]
	public class OpenSleep : Command, INoCommand
	{
		// Token: 0x06006F96 RID: 28566 RVA: 0x002A71C9 File Offset: 0x002A53C9
		public override void OnEnter()
		{
			ISleepMag.Inst.Sleep();
			this.Continue();
		}
	}
}
