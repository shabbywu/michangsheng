using System;
using script.MenPaiTask.ZhangLao.UI;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F9C RID: 3996
	[CommandInfo("YSTools", "打开长老任务界面", "打开长老任务界面", 0)]
	[AddComponentMenu("")]
	public class OpenZhangLaoTask : Command, INoCommand
	{
		// Token: 0x06006F9C RID: 28572 RVA: 0x002A723F File Offset: 0x002A543F
		public override void OnEnter()
		{
			ElderTaskUIMag.Open();
			this.Continue();
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}
	}
}
