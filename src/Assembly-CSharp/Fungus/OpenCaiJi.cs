using System;
using CaiJi;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001443 RID: 5187
	[CommandInfo("YSTools", "OpenCaiJi", "打开采集界面", 0)]
	[AddComponentMenu("")]
	public class OpenCaiJi : Command
	{
		// Token: 0x06007D5C RID: 32092 RVA: 0x00054C97 File Offset: 0x00052E97
		public override void OnEnter()
		{
			ResManager.inst.LoadPrefab("CaiJiPanel").Inst(null);
			CaiJiUIMag.inst.OpenCaiJi(this.ID);
			this.Continue();
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ADD RID: 27357
		[Tooltip("采集界面对应流水号")]
		[SerializeField]
		protected int ID = 1;
	}
}
